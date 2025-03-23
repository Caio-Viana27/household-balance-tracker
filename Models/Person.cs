namespace Household_Balance_Tracker.Models;

public class Person
{
    private int id;
    private string name { get; set; }
    private int age { get; set; }
    private string email { get; }

    public Person(int id, string name, int age, string email)
    {
        this.id = id;
        this.name = name;
        this.age = age;
        this.email = email;
    }
}