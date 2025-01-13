using Portfolio_Backend.Interfaces;
namespace Portfolio_Backend.Services;

public class CustomLogger: ICustomLogger{

    ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("Program");


    public void LogToTerminal(Object msg, int colorOpt = 3){
        switch (colorOpt){

            case 2:
                this.logger.LogDebug(msg.ToString());
                break;
            case 3:
                this.logger.LogError(msg.ToString());
                break;
            case 4:
                this.logger.LogCritical(msg.ToString());
                break;
            case 1:
            default:
                this.logger.LogInformation(msg.ToString());
                break;

        }
    }
    
}