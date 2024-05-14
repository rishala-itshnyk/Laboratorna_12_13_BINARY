namespace Laboratorna_12_13_BINARY.Tests;

public class Tests
{
    Trie trie;

    [SetUp]
    public void Setup()
    {
        trie = new Trie();
    }

    [Test]
    public void Insert_ValidData_Success()
    {
        // Arrange
        string surname = "Порошенко";
        int[] grades = { 80, 85, 90 };

        // Act
        trie.Insert(surname, grades);

        // Assert
        Assert.IsTrue(trie.Search(surname, out int[] result));
        Assert.AreEqual(grades.Length, result.Length);
        Assert.IsTrue(grades.SequenceEqual(result));
    }

    [Test]
    public void Search_ExistingSurname_ReturnsTrue()
    {
        // Arrange
        string surname = "Порошенко";
        int[] grades = { 75, 80, 85 };
        trie.Insert(surname, grades);

        // Act
        bool result = trie.Search(surname, out int[] foundGrades);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNotNull(foundGrades);
        Assert.AreEqual(grades.Length, foundGrades.Length);
        Assert.IsTrue(grades.SequenceEqual(foundGrades));
    }

    [Test]
    public void Search_NonExistingSurname_ReturnsFalse()
    {
        // Arrange
        string surname = "Порошенко";

        // Act
        bool result = trie.Search(surname, out _);

        // Assert
        Assert.IsFalse(result);
    }
    

    [Test]
    public void Delete_NonExistingSurname_ReturnsFalse()
    {
        // Arrange
        string surname = "Порошенко";

        // Act
        bool result = trie.Delete(surname);

        // Assert
        Assert.IsFalse(result);
    }
}