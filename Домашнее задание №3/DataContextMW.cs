using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Avalonia.Controls;
using System.Runtime.CompilerServices;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data;
using Microsoft.CodeAnalysis.CSharp;

namespace dz3;

public class DataContextMainWindow : INotifyPropertyChanged
{
    private double _number;
    private double _number2;

    public enum Operation : int
    {
        dot = 11,
        plus,
        minus,
        multiply,
        divide,
        modulus,
        power,
        factorial,
        sine,
        cosine,
        tangent,
        equal,
        ceiling,
        floor,
        lg,
        ln,
        delete,
        clear,
        negative
    }

    private bool dotFlag = false;
    private ulong counter = 10;
    private int _operation;
    private double _number1 = 0;
    private bool minusflag = false;
    private bool errflag = false;
    private bool negflag = false;
    private string? ErrType;

    public string OutputScreen
    {
        get
        {   
            if(!errflag)
            {
                return _number.ToString();
            }
            else
            { 
                return "Error: " + ErrType;
            }
            
        }
    }

    public string OutputScreenLHS
    {
        get
        {   
            if(!errflag)
            {
                if (_operation < (int)Operation.factorial && _operation > 0)
                    return _number2.ToString();
                else if (_operation == (int)Operation.factorial) 
                {
                    _number2 = Number;
                    return _number2.ToString();
                }
                
                else return "";
            }
            else
            { 
                return "Err";
            }
            
        }
    }

    public string OutputScreenRHS
    {
        get
        {
            if (!errflag)
            {
                if (_operation < (int)Operation.factorial && _operation > 0)
                    return _number1.ToString();
                else if (_operation != 0 && _operation != (int)Operation.factorial && _operation != (int)Operation.delete) return "(" + _number.ToString() + ")";
                else return "";
            } 
            else
            { 
                return "Err.";
            }
            
        }
    }

    public string OutputScreenOPR
    {
        get
        {   
            if(!errflag)
            {
                switch (_operation)
                {
                    case (int)Operation.plus:
                        return "+";
                    case (int)Operation.minus:
                        return "-";
                    case (int)Operation.multiply:
                        return "*";
                    case (int)Operation.divide:
                        return "/";
                    case (int)Operation.modulus:
                        return "%";
                    case (int)Operation.power:
                        return "^";
                    case (int)Operation.factorial:
                        return "!";
                    case (int)Operation.sine:
                        return "sin";
                    case (int)Operation.cosine:
                        return "cos";
                    case (int)Operation.tangent:
                        return "tan";
                    case (int)Operation.equal:
                        return "=";
                    case (int)Operation.ceiling:
                        return "ceiling";
                    case (int)Operation.floor:
                        return "floor";
                    case (int)Operation.lg:
                        return "lg";
                    case (int)Operation.ln:
                        return "ln";
                    default:
                    return "";
                }
            }
            else
            { 
                return "Err";
            }
            
        }
    }

    public double Number
    {
        get
        {
            return _number;
        }
        set
        {   
            _ = SetField(ref _number, value);
        }
    }

    private double NumberHardSet
    {
        set
        {        
            _ = HardSetField(ref _number, value);
        }

    }

    public void ExecuteCommandNumber(string? text)
    {   
        if (errflag) return;
        double number = Convert.ToDouble(text);
        if(number >= 0  && number <= 10)
        {
            Number = number;
        } 
        else if (number == (double)Operation.dot)
        {
            dotFlag = true;
        }
        Console.WriteLine(Number.ToString() + " is inputted ");
    }

    public double Factorial(double number)
    {
        double result = 1;
        if (number > 500) return double.PositiveInfinity;
        for (int i = 1; i <= number; i++)
        {
            result *= i;
        }
        return result;
    }

    public int NumberOfDecimals(double number)
    {
        string numberString = number.ToString();
        int index = numberString.IndexOf(",");
        Console.WriteLine(numberString + " is inputted, index is " + index);
        if (index == -1)
        {
            return 0;
        }
        else
        {
            Console.WriteLine("string length is " + numberString.Length);
            return numberString.Length - index - 1;
        }
    }
    public void ExecuteCommandOperation(string? text)
    {
        int op = Convert.ToInt32(text);

        if (op == (int)Operation.negative)
        {
            negflag = true;
            return;
        }

        if (errflag && op != (int)Operation.clear) return;
    
        if (op == (int)Operation.equal)
        {

            if (_operation != 0)
            {
                dotFlag = false;
                counter = 10;
            }
            switch (_operation)
            {
                case (int)Operation.plus:
                    NumberHardSet = _number2 + _number1;
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.minus:
                    NumberHardSet = _number2 - _number1;
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.multiply:
                    NumberHardSet = _number2 * _number1;
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.divide:
                    if (_number1 == 0)
                    {
                        errflag = true;
                        ErrType = "Can't divide by 0";
                        OnPropertyChanged(nameof(OutputScreen));
                        break;
                    }
                    NumberHardSet = _number2 / _number1;
                    Console.WriteLine("State of dot flag: " + dotFlag + " and counter: " + counter);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    Console.WriteLine("State of dot flag: " + dotFlag + " and counter: " + counter);
                    break;
                case (int)Operation.modulus:
                    if (_number1 == 0)
                    {
                        errflag = true;
                        ErrType = "Can't divide by 0";
                        OnPropertyChanged(nameof(OutputScreen));
                        break;
                    }
                    NumberHardSet = _number2 % _number1;
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.power:
                    NumberHardSet = Math.Pow(_number2, _number1);
                    Console.WriteLine("Number is " + Number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
            }

            _number2 = _number;
            OnPropertyChanged(nameof(OutputScreenLHS));
            OnPropertyChanged(nameof(OutputScreenRHS));
            return;
        }
        else
        {   
            if((_operation != 0 || _operation == (int)Operation.equal) && !errflag){
                if (op != (int)Operation.delete)
                {
                    ExecuteCommandOperation(Convert.ToString((int)Operation.equal));
                    OnPropertyChanged(nameof(OutputScreen));
                }   
            }

            int delete_operation_buffer = _operation;
            _operation = op;
            OnPropertyChanged(nameof(OutputScreenOPR));

            switch (_operation)
            {
                case (int)Operation.factorial:
                    OnPropertyChanged(nameof(OutputScreenLHS));
                    NumberHardSet = Factorial(_number);
                    dotFlag = false;
                    counter = 10;                    
                    break;
                case (int)Operation.sine:
                    NumberHardSet = Math.Sin(_number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }                    
                    break;
                case (int)Operation.cosine:
                    NumberHardSet = Math.Cos(_number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.tangent:
                    NumberHardSet = Math.Tan(_number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number));
                    }
                    break;
                case (int)Operation.ceiling:
                    NumberHardSet = Math.Ceiling(_number);
                    dotFlag = false;
                    counter = 10;
                    break;
                case (int)Operation.floor:
                    NumberHardSet = Math.Floor(_number);
                    dotFlag = false;
                    counter = 10;
                    break;
                case (int)Operation.lg:
                    NumberHardSet = Math.Log10(_number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number)+1);
                    }
                    break;
                case (int)Operation.ln:
                    NumberHardSet = Math.Log(_number);
                    if(NumberOfDecimals(Number) != 0)
                    {
                        dotFlag = true;
                        counter = (ulong)Math.Pow(10, NumberOfDecimals(Number)+1);
                    }
                    break;              
                case (int)Operation.delete:
                    if (dotFlag)
                    {
                        if (Number == 0)
                        {   
                            _operation = delete_operation_buffer;
                            break;
                        }

                        
                        _number *= counter / 10;
                        Console.WriteLine("number1: " + _number + " counter: " + counter);
                        double digit = _number % 10;
                        if (digit == 0)
                        {

                            while (digit == 0 && _number > 0)
                            {  
                                _number /= 10;
                                counter /= 10;
                                digit = _number % 10;
                                Console.WriteLine("loop number1: " + _number + " counter: " + counter);
                            }
                        }
 
                            _number /= 10;
                        Console.WriteLine("mathfl number1: " + Math.Floor(_number) + "counter: " + counter);
                        if (_number >= 0)
                        {
                            NumberHardSet = Math.Floor(_number) * 100 / counter;
                        }
                        else
                        {
                            NumberHardSet = Math.Ceiling(_number) * 100 / counter;
                        }
                        counter /= 10;
                        if(counter == 10) dotFlag = false;
                        Console.WriteLine("number: " + _number);
                    }
                    else
                    {
                        if (_number >= 0)
                        {
                            NumberHardSet = Math.Floor(_number / 10);
                        }
                        else
                        {
                            NumberHardSet =  Math.Ceiling(_number / 10);
                        }
                        if(_number == -0)
                        {
                            NumberHardSet = 0;
                        }
                        
                    }
                    _operation = delete_operation_buffer; 
                    HardSetField(ref _number2, Number);
                    OnPropertyChanged(nameof(OutputScreenLHS)); 
                    OnPropertyChanged(nameof(OutputScreenOPR)); 
                    break;
                case (int)Operation.clear:
                    errflag = false;
                    dotFlag = false;
                    minusflag = false;
                    NumberHardSet = 0;
                    counter = 10;
                    _number2 = 0;
                    _number1 = 0;
                    _operation = 0;
                    OnPropertyChanged(nameof(OutputScreen));   
                    OnPropertyChanged(nameof(OutputScreenLHS));
                    OnPropertyChanged(nameof(OutputScreenRHS));
                    OnPropertyChanged(nameof(OutputScreenOPR));                 
                    break;
                default:
                    dotFlag = false;
                    counter = 10;
                    _number2 = _number;
                    OnPropertyChanged(nameof(OutputScreenLHS));
                    if (op == (int)Operation.minus && Number == 0)
                    {
                        minusflag = true;
                    }
                    else
                    {
                        NumberHardSet = 0;
                    }
                break;
            
            }
            OnPropertyChanged(nameof(OutputScreenLHS));
            OnPropertyChanged(nameof(OutputScreenRHS));
        }
        Console.WriteLine(_number2 + " " + text + " " + _number1 + "( " + _number + " ) ");
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected bool SetField(ref double field, double value, [CallerMemberName] string? propertyName = null)
    {
        if (errflag) return false;
        if (double.IsNaN(value) || value == double.NegativeInfinity)
        {
            errflag = true;
            ErrType = "Invalid lg/ln input.Positive numbers only.";
            OnPropertyChanged(nameof(OutputScreen));
            return false;
        }
        else if (value == double.PositiveInfinity)
        {
            errflag = true;
            ErrType = "Number is too big.";
            OnPropertyChanged(nameof(OutputScreen));
            return false;
        }

        if (field < 0 || minusflag == true)
        {
            if (dotFlag)
            {
                field *= counter;
                field -= value;
                field /= counter;
                counter *= 10;
            }
            else
            {
                field *= 10;
                field -= value;
            }
            minusflag = false;
        }
        else if (dotFlag)
        {
            field *= counter;
            field += value;
            field /= counter;
            counter *= 10;
        }
        else
        {
            field *= 10;
            field += value;
        }

        if(negflag)
        {
            field = -field;
            negflag = false;
        }

        //OnPropertyChanged(nameof(propertyName));
        OnPropertyChanged(nameof(OutputScreen));
        _number1 = field;
        OnPropertyChanged(nameof(OutputScreenRHS));
        return true;
    }
    protected bool HardSetField(ref double field, double value, [CallerMemberName] string? propertyName = null)
    {   
        if (errflag) return false;
        if (value == double.NaN || value == double.NegativeInfinity)
        {
            errflag = true;
            ErrType = "Invalid lg/ln/tg input.";
            OnPropertyChanged(nameof(OutputScreen));
            return false;
        }
        else if (value == double.PositiveInfinity)
        {
            errflag = true;
            ErrType = "Number is too big.";
            OnPropertyChanged(nameof(OutputScreen));
            return false;
        }
        if(negflag)
        {
            field = -value;
            negflag = false;
        }
        else
        {
            field = value;
        }
        //OnPropertyChanged(nameof(propertyName));
        OnPropertyChanged(nameof(OutputScreen));
        OnPropertyChanged(nameof(OutputScreenRHS));
        return true;
    }
}