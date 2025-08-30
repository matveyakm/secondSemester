// <copyright file="SkipListTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyList.Test;

/// <summary>
/// tests for skip list.
/// </summary>
public class SkipListTest
{
    /// <summary>
    /// initialization skip list with elements.
    /// </summary>
    [Test]
    public void SkipList_Initialization_WithElements()
    {
        var testedList = new SkipList<double>([0.56, 1.78, 5.2]);
        double[] expected = [0.56, 1.78, 5.2];

        Assert.That((double[])[testedList[0], testedList[1], testedList[2]], Is.EqualTo(expected));
    }

    /// <summary>
    /// Count after adding new elements in list.
    /// </summary>
    [Test]
    public void SkipList_Count_CorrectAmountAfterAddingNewElements()
    {
        var testedList = new SkipList<string>();
        const int expected = 7;

        testedList.Add("gqrqadga");
        testedList.Add("vsfxg43gef");
        testedList.Add("ifsxfdxvgsn");
        testedList.Add("nsxgavgsisxgs");
        testedList.Add("vssdgsdgfef");
        testedList.Add("ivgwrrwtanw");
        testedList.Add("twenawtvwtitwt");

        Assert.That(testedList.Count, Is.EqualTo(expected));
    }

    /// <summary>
    /// index of existing item.
    /// </summary>
    [Test]
    public void SkipList_IndexOf()
    {
        var testedList = new SkipList<string>(["matvey", "akimenko", "cool"]);
        var result = testedList.IndexOf("akimenko");

        Assert.That(result, Is.EqualTo(0));
    }

    /// <summary>
    /// enumerator should return all elements.
    /// </summary>
    [Test]
    public void SkipList_GetEnumerator_ReturnsAllElements()
    {
        var tested = new SkipList<int>((int[])[124, 543, 893]);
        var result = new List<int>();

        foreach (var item in tested)
        {
            result.Add(item);
        }

        Assert.That(result, Is.EquivalentTo((int[])[124, 543, 893]));
    }

    /// <summary>
    /// modification collection while iteration should throw exception.
    /// </summary>
    [Test]
    public void SkipList_GetEnumerator_ThrowsInvalidOperationException()
    {
        var testedList = new SkipList<char>((char[])['y', 'w', 'z']);
        var enumerator = testedList.GetEnumerator();

        enumerator.MoveNext();
        testedList.Add('r');

        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());

        enumerator.Dispose();
    }

    /// <summary>
    /// add new elements in skip list.
    /// </summary>
    [Test]
    public void SkipList_Add_NewElements()
    {
        var testedList = new SkipList<char>();
        char[] expected = ['a', 'o', 't'];

        testedList.Add('a');
        testedList.Add('o');
        testedList.Add('t');

        Assert.That((char[])[testedList[0], testedList[1], testedList[2]], Is.EqualTo(expected));
    }

    /// <summary>
    /// correct value of Count after clearing list.
    /// </summary>
    [Test]
    public void SkipList_Clear_ShouldResetCountToZero()
    {
        var testedList = new SkipList<int>([1, 2, 3]);
        Assert.That(testedList.Count, Is.EqualTo(3));

        testedList.Clear();

        Assert.That(testedList.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// skip list contains item.
    /// </summary>
    [Test]
    public void SkipList_Contains_ShouldReturnTrue()
    {
        var testedList = new SkipList<int> { 100, 1135, 13421, 134 };

        Assert.That(testedList.Contains(1135), Is.True);
    }

    /// <summary>
    /// remove element.
    /// </summary>
    [Test]
    public void SkipList_Remove_ExistingItem_ReturnsTrueAndRemovesItem()
    {
        var testedList = new SkipList<string>(["matvey", "akimenko"]);

        var result = testedList.Remove("matvey");

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(testedList.Contains("matvey"), Is.False);
            Assert.That(testedList.Count, Is.EqualTo(1));
        });
    }

    /// <summary>
    /// test for correct removing non-existing item.
    /// </summary>
    [Test]
    public void SkipList_Remove_NonExistingItem_ReturnsFalse()
    {
        var list = new SkipList<int>([124, 232, 23523]);
        var result = list.Remove(4);

        Assert.That(result, Is.False);
    }

    /// <summary>
    /// add two thousand elements.
    /// </summary>
    [Test]
    public void SkipList_Add_TwoThousandElements()
    {
        var testedList = new SkipList<int>();

        for (var i = 1; i <= 2000; ++i)
        {
            testedList.Add(i);
        }

        Assert.That(testedList.Contains(1001), Is.True);
    }
}