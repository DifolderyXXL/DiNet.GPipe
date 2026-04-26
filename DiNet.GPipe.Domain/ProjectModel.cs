namespace DiNet.GPipe.Domain;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GitUrl { get; set; }


    public List<BuildRegistry> Builds { get; set; } = [];
}