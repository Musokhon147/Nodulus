using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Luxodd.Game;
using Luxodd.Game.Scripts.Network;
using Luxodd.Game.Scripts.Input;
using UnityEngine;
using View.Game;
using View.Items;

namespace View.Control
{
    /// <summary>
    /// Adapts physical arcade controls to Nodulus gameplay.
    /// Handles node navigation with joystick and actions with buttons.
    /// </summary>
    public class NodulusArcadeAdapter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BoardAction _boardAction;
        [SerializeField] private PuzzleState _puzzleState;
        
        [Header("Settings")]
        [SerializeField] private float _joystickDeadzone = 0.5f;
        [SerializeField] private float _joystickRepeatDelay = 0.3f;

        private NodeView _selectedNode;
        private float _lastMoveTime;
        private bool _joystickCentered = true;

        private void Start()
        {
            FindDependencies();
        }

        private void FindDependencies()
        {
            // First, check the current GameObject
            if (_boardAction == null) _boardAction = GetComponent<BoardAction>();
            if (_puzzleState == null) _puzzleState = GetComponent<PuzzleState>();

            // If still missing, check parent or children (useful for different prefab structures)
            if (_boardAction == null) _boardAction = GetComponentInParent<BoardAction>() ?? GetComponentInChildren<BoardAction>();
            if (_puzzleState == null) _puzzleState = GetComponentInParent<PuzzleState>() ?? GetComponentInChildren<PuzzleState>();

            if (_boardAction == null || _puzzleState == null)
            {
                Debug.LogWarning($"[NodulusArcadeAdapter] Some dependencies are missing! BoardAction: {_boardAction}, PuzzleState: {_puzzleState}");
            }
        }

        private void Update()
        {
            HandleNavigation();
            HandleActions();
        }

        private void HandleNavigation()
        {
            ArcadeStick stick = ArcadeControls.GetStick();
            Vector2 vector = stick.Vector;

            // Simple deadzone and centering logic
            if (vector.magnitude < _joystickDeadzone)
            {
                _joystickCentered = true;
                return;
            }

            if (!_joystickCentered && Time.time - _lastMoveTime < _joystickRepeatDelay)
            {
                return;
            }

            // Determine direction from joystick
            Direction dir = Direction.None;
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            {
                dir = vector.x > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                dir = vector.y > 0 ? Direction.Up : Direction.Down;
            }

            if (dir != Direction.None)
            {
                MoveSelection(dir);
                _lastMoveTime = Time.time;
                _joystickCentered = false;
            }
        }

        private void MoveSelection(Direction dir)
        {
            // If no node is selected, try to select the start node or first node
            if (_selectedNode == null)
            {
                _selectedNode = _puzzleState.PlayerNodes.FirstOrDefault() ?? _puzzleState.NonPlayerNodes.FirstOrDefault();
                if (_selectedNode == null) return;
            }

            // Find nearest node in target direction
            Point currentPos = _selectedNode.Node.Position;
            Point targetPos = currentPos + dir.ToPoint();
            
            // In Nodulus, nodes are on integer coordinates.
            // We search for a node at the target position, or continue searching in that direction.
            NodeView nextNode = null;
            
            // Search up to 10 units in that direction
            for (int i = 1; i <= 10; i++)
            {
                Point checkPos = currentPos + (dir.ToPoint() * i);
                nextNode = GetNodeAt(checkPos);
                if (nextNode != null) break;
            }

            if (nextNode != null)
            {
                if (_selectedNode != null) _selectedNode.Highlight(false);
                _selectedNode = nextNode;
                HighlightSelection();
            }
        }

        private NodeView GetNodeAt(Point pos)
        {
            // We need a way to find a node by point. 
            // PuzzleState has _nodeMap but it's private. However, it's used in many places.
            // We can use a trick: search all nodes.
            return _puzzleState.PlayerNodes.Concat(_puzzleState.NonPlayerNodes)
                .FirstOrDefault(n => n.Node.Position.Equals(pos));
        }

        private void HighlightSelection()
        {
            if (_selectedNode == null) return;
            
            Debug.Log($"[Arcade] Selected Node at {_selectedNode.Node.Position}");
            
            // Visual feedback: Use the game's built-in highlight system
            _selectedNode.Highlight(true);
        }

        private void HandleActions()
        {
            if (_selectedNode == null) return;

            // Black Button: Rotate/Pull/Push in a specific direction? 
            // In the original game, swipe direction determines action.
            // For Arcade, we can use the Joystick direction HELD when pressing a button, 
            // or just use specific buttons for specific directions.
            
            // Suggestion: 
            // Black Button + Joystick = Action in that direction.
            // Red Button = Undo/Cancel.
            
            if (ArcadeControls.GetButtonDown(ArcadeButtonColor.Black))
            {
                // Determine direction for the move
                ArcadeStick stick = ArcadeControls.GetStick();
                Direction moveDir = GetDirectionFromVector(stick.Vector);
                
                if (moveDir != Direction.None)
                {
                    _boardAction.Play(_selectedNode, moveDir);
                }
                else
                {
                    // If no stick held, maybe just "Tap" (which doesn't do much in Nodulus yet)
                    _boardAction.Play(_selectedNode);
                }
            }
            
            // Allow Red button to also trigger moves if joystick is held
            if (ArcadeControls.GetButtonDown(ArcadeButtonColor.Red))
            {
                 ArcadeStick stick = ArcadeControls.GetStick();
                 Direction moveDir = GetDirectionFromVector(stick.Vector);
                 if (moveDir != Direction.None)
                 {
                     _boardAction.Play(_selectedNode, moveDir);
                 }
            }
        }

        private Direction GetDirectionFromVector(Vector2 vector)
        {
            if (vector.magnitude < 0.2f) return Direction.None;
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            {
                return vector.x > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                return vector.y > 0 ? Direction.Up : Direction.Down;
            }
        }
    }
}
