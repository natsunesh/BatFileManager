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
    public string Description => "Открыть сайт: " + _url;
    public string ToBatCommand() => "start " + _url;
}

public class CloseProcessAction : IAction
{
    private readonly string _processName;
    public CloseProcessAction(string processName) { _processName = processName; }
    public string Description => "Закрыть процесс: " + _processName;
    public string ToBatCommand() => "taskkill /IM " + _processName + ".exe /F";
}

public class RunProgramAction : IAction
{
    private readonly string _programPath;
    public RunProgramAction(string programPath) { _programPath = programPath; }
    public string Description => "Запустить программу: " + _programPath;
    public string ToBatCommand() => "start \"\" \"" + _programPath + "\"";
}

public class OpenFolderAction : IAction
{
    private readonly string _folderPath;
    public OpenFolderAction(string folderPath) { _folderPath = folderPath; }
    public string Description => "Открыть папку: " + _folderPath;
    public string ToBatCommand() => "start \"\" \"" + _folderPath + "\"";
}

public class DelayAction : IAction
{
    private readonly int _seconds;
    public DelayAction(int seconds) { _seconds = seconds; }
    public string Description => "Задержка: " + _seconds + " сек";
    public string ToBatCommand() => "timeout /t " + _seconds;
}

public class CopyFileAction : IAction
{
    private readonly string _source;
    private readonly string _destination;
    public CopyFileAction(string source, string destination) { _source = source; _destination = destination; }
    public string Description => "Копировать файл: " + _source + " -> " + _destination;
    public string ToBatCommand() => "copy \"" + _source + "\" \"" + _destination + "\"";
}

public class MoveFileAction : IAction
{
    private readonly string _source;
    private readonly string _destination;
    public MoveFileAction(string source, string destination) { _source = source; _destination = destination; }
    public string Description => "Переместить файл: " + _source + " -> " + _destination;
    public string ToBatCommand() => "move \"" + _source + "\" \"" + _destination + "\"";
}

public class DeleteFileAction : IAction
{
    private readonly string _filePath;
    public DeleteFileAction(string filePath) { _filePath = filePath; }
    public string Description => "Удалить файл: " + _filePath;
    public string ToBatCommand() => "del /F /Q \"" + _filePath + "\"";
}

public class CreateFolderAction : IAction
{
    private readonly string _folderPath;
    public CreateFolderAction(string folderPath) { _folderPath = folderPath; }
    public string Description => "Создать папку: " + _folderPath;
    public string ToBatCommand() => "mkdir \"" + _folderPath + "\"";
}

public class ListProcessesAction : IAction
{
    public ListProcessesAction() { }
    public string Description => "Список процессов";
    public string ToBatCommand() => "tasklist";
}

public class ShutdownAction : IAction
{
    private readonly int _minutes;
    public ShutdownAction(int minutes = 0) { _minutes = minutes; }
    public string Description => "Выключить ПК через " + _minutes + " мин";
    public string ToBatCommand() => "shutdown /s /t " + (_minutes * 60);
}

public class ClearScreenAction : IAction
{
    public ClearScreenAction() { }
    public string Description => "Очистить экран";
    public string ToBatCommand() => "cls";
}