// <copyright file="BwtTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace BWT.Test;

/// <summary>
/// NUnit-based tests for BWT.
/// </summary>
[TestFixture]
public class BwtTest
{
    /// <summary>
    /// test transform with upper case string.
    /// </summary>
    [Test]
    public void Bwt_Transform_UpperCaseString()
        => Assert.That(Bwt.Transform("HELLOWORLD"), Is.EqualTo(("LHDRELWLOO", 3)));

    /// <summary>
    /// test transform with russian string.
    /// </summary>
    [Test]
    public void Bwt_Transform_RussianString()
        => Assert.That(Bwt.Transform("привет,мир"), Is.EqualTo(("тиврм,рпие", 7)));

    /// <summary>
    /// test reverse transform with default string.
    /// </summary>
    [Test]
    public void Bwt_ReverseTransform_DefaultString()
        => Assert.That(Bwt.ReverseTransform("nnbaaa", 4), Is.EqualTo("banana"));

    /// <summary>
    /// test reverse transform with string with special symbols.
    /// </summary>
    [Test]
    public void Bwt_ReverseTransform_StringWithSpecialSymbols()
        => Assert.That(Bwt.ReverseTransform("ta_ttues_sj", 5), Is.EqualTo("just_a_test"));
}
