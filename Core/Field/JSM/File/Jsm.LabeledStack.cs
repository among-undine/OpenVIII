﻿using System;
using System.Collections.Generic;


namespace OpenVIII.Fields.Scripts
{
    public static partial class Jsm
    {
        public sealed class LabeledStack : IStack<IJsmExpression>
        {
            private readonly Stack<IJsmExpression> _stack = new Stack<IJsmExpression>();
            private readonly Dictionary<IJsmExpression, Int32> _positions = new Dictionary<IJsmExpression, Int32>();

            public Int32 Count => _stack.Count;
            public Int32 CurrentLabel { get; set; }

            public void Push(IJsmExpression item)
            {
                _positions.Add(item, CurrentLabel);
                _stack.Push(item);
            }

            public IJsmExpression Peek()
            {
                return _stack.Peek();
            }

            public IJsmExpression Pop()
            {
                IJsmExpression result = _stack.Pop();

                CurrentLabel = _positions[result];
                _positions.Remove(result);

                return result;
            }
        }
    }
}