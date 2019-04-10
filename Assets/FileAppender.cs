using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileAppender : ILogHandler
{
    public static ILogger Create(string logFile, bool outputConsole)
    {
        var logger = new FileAppender(logFile, outputConsole);
        return new Logger(logger);
    }

    StreamWriter writer;
    bool outputConsole;

    private FileAppender(string logFile, bool outputConsole)
    {
        this.writer = File.AppendText(logFile);
        this.outputConsole = outputConsole;
    }

    public void Close()
    {
        this.writer.Close();
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        if (this.outputConsole)
            Debug.unityLogger.LogFormat(logType, context, format, args);

        StringBuilder sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        sb.Append(" [");
        sb.Append(logType.ToString());
        sb.Append("] ");
        sb.Append(string.Format(format, args));
        this.writer.WriteLine(sb.ToString());
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        if (this.outputConsole)
            Debug.unityLogger.LogException(exception, context);

        StringBuilder sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        sb.Append(" [");
        sb.Append(LogType.Exception.ToString());
        sb.Append("] ");
        sb.Append(exception.Message);
        sb.AppendLine();
        sb.Append(exception.StackTrace);
        this.writer.WriteLine(sb.ToString());
    }
}
