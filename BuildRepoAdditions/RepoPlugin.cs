namespace BuildRepoAdditions;

public class RepoPlugin(
    string workingDirectory,
    string readMe,
    string changelog,
    string[] outputDirectories,
    string[] modFiles,
    ModManifest modManifest) {
    
    public string      WorkingDirectory  = workingDirectory;
    public string      ReadMe            = readMe;
    public string      Changelog         = changelog;
    public string[]    OutputDirectories = outputDirectories;
    public string[]    ModFiles          = modFiles;
    public ModManifest ModManifest       = modManifest;
}