using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntCode
{
  public class Executor
  {
    private long _iPtr;
    private long[] _intCode;   //only to keep a local copy of the provided intCode. Used for resetting!
    private long _relBase;                  //relative base for supporting relative mode parameters
    private readonly long _intCodeLen;
    private long[] _memory;
    private Queue<long> _inputQueue;
    private Queue<long> _outputQueue;
    private bool _awaitingInput;

    public Queue<long> InputQueue { get => _inputQueue; set => _inputQueue = value; }

    public Queue<long> OutputQueue { get => _outputQueue; set => _outputQueue = value; }

    public bool AwaitingInput => _awaitingInput;

    public long[] Memory { get => _memory; set => _memory = value; }

    public Executor(long[] intCode)
    {
      _intCodeLen = intCode.Length;
      _memory = new long[_intCodeLen * 100];  //100 times the program
      _memory.Initialize();
      Array.Copy(intCode, _memory, _intCodeLen);
      _intCode = intCode;
      _relBase = 0;
      _iPtr = 0;
      _inputQueue = new Queue<long>();
      _outputQueue = new Queue<long>();
      _awaitingInput = false;
    }

    public void Initialize()
    {
      //reload ROM without doing anything at all to RAM
      Array.Copy(_intCode, _memory, _intCodeLen);
      _iPtr = 0;
    }

    public long[] Execute()
    {
      bool keepGoing = true;

      while (keepGoing)
      {
        var instruction = InstDecoder.Decode(_memory[_iPtr]);

        switch (instruction.OpCode)
        {
          case Opcodes.Add:
            Add(instruction);
            break;
          case Opcodes.Multiply:
            Multiply(instruction);
            break;
          case Opcodes.Read:
            Read(instruction);
            break;
          case Opcodes.Write:
            Write(instruction);
            break;
          case Opcodes.JumpIfTrue:
            JumpIfTrue(instruction);
            break;
          case Opcodes.JumpIfFalse:
            JumpIfFalse(instruction);
            break;
          case Opcodes.LessThan:
            LessThan(instruction);
            break;
          case Opcodes.Equals:
            Equals(instruction);
            break;
          case Opcodes.RelativeBaseOffset:
            RelativeBaseOffset(instruction);
            break;
          case Opcodes.Exit:
            _iPtr = 0;  //reset
            keepGoing = false;
            break;
          default:
            _iPtr = 0;  
            throw new InvalidOperationException("Unknown Opcode");
        }

        if (_awaitingInput)
          return _memory.Take(Convert.ToInt32(_intCodeLen)).ToArray();
      }

      return _memory.Take(Convert.ToInt32(_intCodeLen)).ToArray();
    }

    public void ResumeExecution()
    {
      if (_awaitingInput)
      {
        _awaitingInput = false;
        Execute();
      }
    }

    private void Add(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Reference)
        _memory[_memory[_iPtr + 3]] = p1 + p2;

      else if (i.p3ParamMode == ParamMode.Value)
        _memory[_memory[_iPtr + 3]] = p1 + p2;
      
      else
        _memory[_memory[_iPtr + 3] + _relBase] = p1 + p2;

      _iPtr += 4;
    }

    private void Multiply(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Reference)
        _memory[_memory[_iPtr + 3]] = p1 * p2;

      else if (i.p3ParamMode == ParamMode.Value)
        _memory[_iPtr + 3] = p1 * p2;

      else
        _memory[_memory[_iPtr + 3] + _relBase] = p1 * p2;

      _iPtr += 4;
    }

    private void Read(Instruction i)
    {
      if (_inputQueue.Count != 0)
      {
        var value = _inputQueue.Dequeue();

        if (i.p1ParamMode == ParamMode.Reference)
          _memory[_memory[_iPtr + 1]] = value;

        else if (i.p1ParamMode == ParamMode.Value)
          _memory[_iPtr + 1] = value;
        
        else
          _memory[_memory[_iPtr + 1] + _relBase] = value;

        _iPtr += 2;
      }
      else
        _awaitingInput = true;
    }

    private void Write(Instruction i)
    {
      long p1;

      if (i.p1ParamMode == ParamMode.Reference)
        p1 = _memory[_memory[_iPtr + 1]];

      else if (i.p1ParamMode == ParamMode.Value)
        p1 = _memory[_iPtr + 1];

      else
        p1 = _memory[_memory[_iPtr + 1] + _relBase];

      _outputQueue.Enqueue(p1);
      _iPtr += 2;
    }

    private void JumpIfTrue(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 != 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing  
    }

    private void JumpIfFalse(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 == 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing
    }

    private void LessThan(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      long val = ((p1 < p2) ? 1 : 0);

      if (i.p3ParamMode == ParamMode.Reference)
        _memory[_memory[_iPtr + 3]] = val;

      else if (i.p3ParamMode == ParamMode.Value)
        _memory[_iPtr + 3] = val;     

      else
        _memory[_memory[_iPtr + 3] + _relBase] = val;

      _iPtr += 4;
    }

    private void Equals(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      long val = ((p1 == p2) ? 1 : 0);
               
      if (i.p3ParamMode == ParamMode.Reference)
        _memory[_memory[_iPtr + 3]] = val;

      else if (i.p3ParamMode == ParamMode.Value)
        _memory[_iPtr + 3] = val;

      else
        _memory[_memory[_iPtr + 3] + _relBase] = val;
      
      _iPtr += 4;
    }

    private void RelativeBaseOffset(Instruction i)
    {
      long p1;
      
      if (i.p1ParamMode == ParamMode.Reference)
        p1 = _memory[_memory[_iPtr + 1]];

      else if (i.p1ParamMode == ParamMode.Value)
        p1 = _memory[_iPtr + 1];

      else  
        p1 = _memory[_memory[_iPtr + 1] + _relBase];

      _relBase += p1;  //update relative base

      _iPtr += 2;
    }

    private Tuple<long, long> GetParameters(Instruction i)
    {
      long p1, p2;
               
      if (i.p1ParamMode == ParamMode.Reference)
        p1 = _memory[_memory[_iPtr + 1]];

      else if (i.p1ParamMode == ParamMode.Value)
        p1 = _memory[_iPtr + 1];

      else 
        p1 = _memory[_memory[_iPtr + 1] + _relBase];

      if (i.p2ParamMode == ParamMode.Reference)
        p2 = _memory[_memory[_iPtr + 2]];

      else if (i.p2ParamMode == ParamMode.Value)
        p2 = _memory[_iPtr + 2];

      else
        p2 = _memory[_memory[_iPtr + 2] + _relBase];

      return Tuple.Create(p1, p2);
    }
  }
}