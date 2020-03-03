namespace IntCode
{
  public enum Opcodes
  {
    Add,                 //01
    Multiply,            //02
    Read,                //03
    Write,               //04
    JumpIfTrue,          //05
    JumpIfFalse,         //06
    LessThan,            //07
    Equals,              //08
    RelativeBaseOffset,  //09
    Exit                 //99
  }
}