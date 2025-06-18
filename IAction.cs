using System.Collections.Generic;

public interface IAction
{
    string Description { get; }
    string ToBatCommand();
}

public class OpenSiteAction : IAction
{
    private readonly string _url;
    public OpenSiteAction(string url) { _url = url; }
    public string Description => "������� ����: " + _url;
    public string ToBatCommand() => "start " + _url;
}

public class CloseProcessAction : IAction
{
    private readonly string _processName;
    public CloseProcessAction(string processName) { _processName = processName; }
    public string Description => "������� �������: " + _processName;
    public string ToBatCommand() => "taskkill /IM " + _processName + ".exe /F";
}

public class RunProgramAction : IAction
{
    private readonly string _programPath;
    public RunProgramAction(string programPath) { _programPath = programPath; }
    public string Description => "��������� ���������: " + _programPath;
    public string ToBatCommand() => "start \"\" \"" + _programPath + "\"";
}

public class OpenFolderAction : IAction
{
    private readonly string _folderPath;
    public OpenFolderAction(string folderPath) { _folderPath = folderPath; }
    public string Description => "������� �����: " + _folderPath;
    public string ToBatCommand() => "start \"\" \"" + _folderPath + "\"";
}

public class DelayAction : IAction
{
    private readonly int _seconds;
    public DelayAction(int seconds) { _seconds = seconds; }
    public string Description => "��������: " + _seconds + " ���";
    public string ToBatCommand() => "timeout /t " + _seconds;
}
