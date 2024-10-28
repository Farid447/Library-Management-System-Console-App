namespace Library_Management_System;

internal abstract class Person
{
    static int _idcounter;
    private string _name;

    public int Id { get; }
    public string Name { get { return _name; } set { _name = value; } }

    protected Person(string name)
    {
        Id = ++_idcounter;
        _name = name;
    }

}

internal class Librarian : Person
{
    private DateTime _hiredate;
    public DateTime HireDate { get { return _hiredate; } set { _hiredate = value; } }

    public Librarian(string name, DateTime hiredate) : base(name)
    {
        HireDate = hiredate;
    }

}

internal sealed class LibraryMember : Person
{
    private DateTime _membershipdate;
    public DateTime MembershipDate { get { return _membershipdate; } set { _membershipdate = value; } }

    public LibraryMember(string name, DateTime membershipdate) : base(name)
    {
        MembershipDate = membershipdate;
    }

}

internal abstract class LibraryItem
{
    static int idcount;
    public int Id { get; set; }
    //private string _title;
    private DateTime? _publicationyear;
    public string Title { get; set; }
    public DateTime? PublicationYear { get { return _publicationyear; } set { _publicationyear = value; } }

    protected LibraryItem(string title, DateTime? publicationyear)
    {
        Id = ++idcount;
        Title = title;
        PublicationYear = publicationyear;
    }

    public abstract void DisplayInfo();


}

internal class Book : LibraryItem
{
    private BookGenre Genre;

    LibraryLocation Location;

    public Book(string title, DateTime? publicationyear, BookGenre genre, LibraryLocation location) : base(title, publicationyear)
    {
        Genre = genre;
        Location = location;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(Genre + " " + Location.Shelf + " " + Location.Aisle);
    }
}

internal class Magazine : LibraryItem
{
    LibraryLocation Location;


    public Magazine(string title, DateTime? publicationyear, LibraryLocation location) : base(title, publicationyear)
    {
        Location = location;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(Location.Shelf + " " + Location.Aisle);
    }
}

internal class DVD : LibraryItem
{
    LibraryLocation Location;


    public DVD(string title, DateTime? publicationyear, LibraryLocation location) : base(title, publicationyear)
    {
        Location = location;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(Location.Shelf + " " + Location.Aisle);
    }
}

struct LibraryLocation
{
    public int Aisle { get; set; }
    public int Shelf { get; set; }

    public LibraryLocation(int aisle, int shelf)
    {
        Aisle = aisle;
        Shelf = shelf;
    }

}
enum BookGenre
{
    Fiction = 1,
    NonFiction,
    Science,
    Art
}

internal static class LibraryHelper
{
    public static System.TimeSpan? CalculateAge(this LibraryItem item)
    {
        DateTime datenow = DateTime.Now;

        return datenow - item.PublicationYear;
    }

    public static void ToTitleCase(this LibraryItem item)
    {
        string item2 = "";
        item2 += item.Title[0];
        for (int i = 1; i < item.Title.Length - 1; i++)
        {
            if (item.Title[i] == ' ' && item.Title[i + 1] <= 122 && item.Title[i + 1] >= 97)
            {
                item2 += " ";
                item2 += (char)(item.Title[i + 1] - 32);
                i++;
            }
        }
        item.Title = item2;
    }
}

internal class LibraryCatalog
{
    LibraryItem[] arr;
    public LibraryCatalog(LibraryItem[] arr)
    {
        this.arr = arr;
    }
    public LibraryItem this[int index]
    {
        get
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (index == arr[i].Id)
                {
                    return arr[i];
                }
            }
            throw new CustomBookErrorexception();
        }
    }
}

internal class CustomBookErrorexception : Exception
{
    public CustomBookErrorexception()
    {
        Console.WriteLine("kitab tapilmadi");
    }
}