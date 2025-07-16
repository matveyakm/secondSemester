using Trie;

public class BorTests
{
    [Test]
    public void TrieAddNewWordsShouldReturnTrue()
    {
        var bor = new Bor();

        List<string> words = ["mother", "moth", "m"];

        foreach (var word in words)
        {
            Assert.That(bor.Add(word), Is.True);
        }
    }

    [Test]
    public void TrieSizeAfterAddingWords()
    {
        var bor = new Bor();

        List<string> words = ["word", "test", "mum", "word_2"];

        foreach (var word in words) {
            bor.Add(word);
        }

        Assert.That(bor.WordCount, Is.EqualTo(words.Count));
    }

    [Test]
    public void TrieAddAlreadyAddedWordShouldReturnFalse()
    {
        var bor = new Bor();

        string word = "Word";

        bor.Add(word);

        Assert.That(bor.Add(word), Is.False);
    }

    [Test]
    public void TrieAddWordAfterDeletingShouldReturnTrue()
    {
        var bor = new Bor();

        string word = "test";

        bor.Add(word);

        bor.Remove(word);

        Assert.That(bor.Add(word), Is.True);
    }

    [Test]
    public void TrieSizeAfterDeleting()
    {
        var bor = new Bor();

        List<string> words = ["word", "test", "Test", "word2"];

        foreach (var word in words) {
            bor.Add(word);
        }

        bor.Remove(words[0]);
        bor.Remove(words[1]);

        int expextedResult = words.Count - 2;

        Assert.That(bor.WordCount, Is.EqualTo(expextedResult));
    }

    [Test]
    public void TrieSizeOfEmptyBor()
    {
        var bor = new Bor();

        int expextedResult = 0;

        Assert.That(bor.WordCount, Is.EqualTo(expextedResult));
    }

    [Test]
    public void TrieContainsWordAfterAddingShouldReturnTrue()
    {
        var bor = new Bor();

        string word = "Word";

        bor.Add(word);

        Assert.That(bor.Contains(word), Is.True);
    }

    [Test]
    public void TrieContainsWordWhichNotAddedShouldReturnFalse()
    {
        var bor = new Bor();

        string word = "test";

        Assert.That(bor.Contains(word), Is.False);
    }

    [Test]
    public void TrieContainsWordAfterDeletingShouldReturnFalse()
    {
        var bor = new Bor();

        string word = "Word";

        bor.Add(word);

        bor.Remove(word);

        Assert.That(bor.Contains(word), Is.False);
    }

    [Test]
    public void TrieRemoveWordAfterAdding()
    {
        var bor = new Bor();

        string word = "Word_123";

        bor.Add(word);

        bor.Remove(word);

        Assert.That(bor.Contains(word), Is.False);
    }

    [Test]
    public void TrieRemoveWordWhichWasNotAddedShouldReturnFalse()
    {
        var bor = new Bor();

        string word = "LLM";

        Assert.That(bor.Remove(word), Is.False);
    }

}