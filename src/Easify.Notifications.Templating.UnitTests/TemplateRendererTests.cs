// This software is part of the Easify framework
// Copyright (C) 2019 Intermediate Capital Group
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Easify.Notifications.Templating.UnitTests;

public class TemplateRendererTests
{
    [Theory]
    [InlineData("Hello {{Title}} test at {{date AsOf}}", "Hello RenderAsync test at 21/09/2018")]
    public async Task GivenTemplate_WhenCalling_RenderTemplateAsync_ShouldReturnCorrectTemplate(string template, string expected)
    {
        // Arrange
        var model = new Model();
        var templateProvider = Substitute.For<ITemplateProvider>();
        templateProvider.GetTemplateContentAsync(Arg.Any<string>()).Returns(Task.FromResult(template));

        var templateContentRenderer = Substitute.For<ITemplateContentRenderer>();
        templateContentRenderer.RenderAsync(Arg.Any<TemplateDefinition>(), model).Returns(Task.FromResult(new RenderedTemplate<Model>(expected, model)));

        var sut = new TemplateRenderer(templateProvider, templateContentRenderer);

        // Act
        var actual = await sut.RenderTemplateAsync("TemplateName", "TemplatePath", model);

        // Assert
        actual.Should().NotBeNull();
        actual.Content.Should().Be(expected);
        actual.Data.Should().Be(model);
    }

    private class Model
    {
        public DateTime AsOf { get; set; } = new DateTime(2018, 9, 21, 4, 30, 30);
        public string Title { get; set; } = "RenderAsync";
    }
}
