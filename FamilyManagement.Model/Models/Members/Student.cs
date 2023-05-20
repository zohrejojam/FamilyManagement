namespace FamilyManagement.Model.Members
{
    public class Student : Member
    {
        public Student(string firstName, string lastName, string gender, string dateOfBirth,string gradeLevel)
            :base(firstName,lastName,gender,dateOfBirth)
        {
            GradeLevel = gradeLevel;
        }
        public string GradeLevel { get; set; }
    }
}
