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

    /// <summary>
    /// add null item should throw argument null exception.
    /// </summary>
    [Test]
    public void SkipList_Add_NullItem_ShouldThrowArgumentNullException()
        => Assert.Throws<ArgumentNullException>(() => new SkipList<string>().Add(null!));
}