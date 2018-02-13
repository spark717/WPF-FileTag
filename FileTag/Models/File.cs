namespace FileTag.Models
{
    public class File
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Resolution { get; set; }

        public override string ToString()
        {
            return Path;
        }
    }
}
