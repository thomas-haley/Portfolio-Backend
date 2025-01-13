namespace Portfolio_Backend.Interfaces;

public interface ICustomLogger{

    void LogToTerminal(Object msg, int colorOpt = 3);
    
}