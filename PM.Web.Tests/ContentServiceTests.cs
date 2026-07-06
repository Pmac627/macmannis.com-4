using System;
using System.IO;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using PM.Web.Services;
using Xunit;

namespace PM.Web.Tests
{
    public class ContentServiceTests
    {
        private const string ValidJson = """
        {
          "hero": { "name": "Pat MacMannis", "roles": ["an Engineer"] },
          "about": { "title": "Engineer", "paragraphs": ["Paragraph one."], "certifications": [], "startYear": 2008 },
          "facts": [ { "icon": "fas fa-user", "value": 50, "label": "Clients", "durationSeconds": 2 } ],
          "skills": [ { "name": "C#", "icon": "fab fa-microsoft", "startYear": 2010 } ],
          "resume": {
            "summary": "Experienced leader.",
            "speakingEngagements": {
              "description": "Guest speaker:",
              "bullets": ["Spring 2013 - Talk"]
            },
            "education": {
              "major": "Bachelor of Arts - History",
              "minorOrCertificate": "Certificate in Computer & Information Sciences",
              "period": "2006 - 2010",
              "location": "Saint Vincent College, Latrobe, PA"
            },
            "certifications": [],
            "experience": []
          },
          "services": [],
          "portfolio": [
            {
              "slug": "tp",
              "title": "TheraPartners",
              "category": "Web Development",
              "client": "TheraPartners, LLC",
              "date": "March 2017",
              "projectUri": null,
              "paragraphs": [],
              "thumbnail": { "uri": "/img/portfolio/therapartners.png", "alt": "", "title": "" },
              "images": [],
              "tags": []
            }
          ],
          "testimonials": []
        }
        """;

        private const string MalformedJson = "{ not valid json ";

        private static ContentService CreateSut(IContentFileReader reader)
        {
            return new ContentService(reader, A.Fake<ILogger<ContentService>>());
        }

        [Fact]
        public void GetContent_WithValidJson_MapsToExpectedModel()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Returns(ValidJson);
            var sut = CreateSut(reader);

            var content = sut.GetContent();

            Assert.Equal("Pat MacMannis", content.Hero.Name);
            Assert.Equal("an Engineer", Assert.Single(content.Hero.Roles));
            Assert.Equal("Engineer", content.About.Title);
            Assert.Equal(2008, content.About.StartYear);
            var fact = Assert.Single(content.Facts);
            Assert.Equal(2, fact.DurationSeconds);
            Assert.Single(content.Skills);
            Assert.Equal("Experienced leader.", content.Resume.Summary);
            Assert.Equal("Guest speaker:", content.Resume.SpeakingEngagements.Description);
            Assert.Equal("Spring 2013 - Talk", Assert.Single(content.Resume.SpeakingEngagements.Bullets));
            Assert.Equal("Bachelor of Arts - History", content.Resume.Education.Major);
            Assert.Equal("Certificate in Computer & Information Sciences", content.Resume.Education.MinorOrCertificate);
            Assert.Equal("2006 - 2010", content.Resume.Education.Period);
            Assert.Equal("Saint Vincent College, Latrobe, PA", content.Resume.Education.Location);
            Assert.Single(content.Portfolio);
        }

        [Fact]
        public void GetContent_WithMissingFile_ThrowsImmediately()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Throws(new FileNotFoundException("site.json not found"));
            var sut = CreateSut(reader);

            Assert.Throws<FileNotFoundException>(() => sut.GetContent());
        }

        [Fact]
        public void GetContent_WithMalformedJson_ThrowsImmediately()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Returns(MalformedJson);
            var sut = CreateSut(reader);

            Assert.ThrowsAny<Exception>(() => sut.GetContent());
        }

        [Fact]
        public void GetContent_CalledMultipleTimes_ReadsFileOnlyOnce()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Returns(ValidJson);
            var sut = CreateSut(reader);

            sut.GetContent();
            sut.GetContent();
            sut.GetContent();

            A.CallTo(() => reader.ReadAllText()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetProject_WithKnownSlug_ReturnsProjectCaseInsensitively()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Returns(ValidJson);
            var sut = CreateSut(reader);

            var project = sut.GetProject("TP");

            Assert.NotNull(project);
            Assert.Equal("TheraPartners", project.Title);
        }

        [Fact]
        public void GetProject_WithUnknownSlug_ReturnsNull()
        {
            var reader = A.Fake<IContentFileReader>();
            A.CallTo(() => reader.ReadAllText()).Returns(ValidJson);
            var sut = CreateSut(reader);

            var project = sut.GetProject("does-not-exist");

            Assert.Null(project);
        }
    }
}
