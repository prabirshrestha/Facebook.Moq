# Facebook.Moq

## Overview
Facebook.Moq is a helper file aimed to ease unit testing for 
[Facebook C# SDK](http://facebooksdk.codeplex.com).

## Requirements

* Moq
* Facebook C# SDK >= v5.0.25

You can use any unit testing library such as MSTests, XUnit.Net or NUnit with Facebook.Moq

# NuGet
Facebook.Moq can also be installed via NuGet.

	Install-Package Facebook.Moq

# Usage
Either copy and add FacebookMock.cs file to your project or using NuGet to install Facebook.Moq.

### Create a Mock instance of FacebookClient

	var mockFb = new Mock<FacebookClient> { CallBase = true };

Make sure to set the CallBase to true as Facebook.Moq is highly dependent on CallBase being enabled.
Shorthand for the above code is

	var mockFb = FacebookMock.New();

## License
Facebook.Moq is intended to be used in both open-source and commercial environments.

Facebook.Moq is licensed under Microsoft Public License (MS-PL).

