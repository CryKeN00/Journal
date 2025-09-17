public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string Subject { get; set; }
    public int GradeValue { get; set; }
    public DateTime Date { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; }
}