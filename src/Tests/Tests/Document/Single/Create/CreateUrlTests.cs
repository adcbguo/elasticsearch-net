﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Single.Create
{
	public class CreateUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project { Name = "NEST" };

			await PUT("/project/_create/1")
				.Fluent(c => c.Create<object>(new { }, i => i.Index(typeof(Project)).Id(1)))
				.Request(c => c.Create(new CreateRequest<object>("project", 1) { Document = new { } }))
				.FluentAsync(c => c.CreateAsync<object>(new { }, i => i.Index(typeof(Project)).Id(1)))
				.RequestAsync(c => c.CreateAsync(new CreateRequest<object>(IndexName.From<Project>(), 1)
				{
					Document = new { }
				}));

			await PUT("/project/_create/NEST")
				.Fluent(c => c.CreateDocument(project))
				.Request(c => c.Create(new CreateRequest<Project>(project)))
				.FluentAsync(c => c.CreateDocumentAsync(project))
				.RequestAsync(c => c.CreateAsync(new CreateRequest<Project>(project)));

			await PUT("/project/_create/NEST")
				.Fluent(c => c.Create(project, cc => cc.Index("project")))
				.Request(c => c.Create(new CreateRequest<Project>(project, "project", "NEST") { Document = project }))
				.RequestAsync(c => c.CreateAsync(new CreateRequest<Project>(project, "project", "NEST") { Document = project }))
				.FluentAsync(c => c.CreateAsync(project, cc => cc.Index("project")));

			await PUT("/different-projects/_create/elasticsearch")
					.Request(c => c.Create(new CreateRequest<Project>("different-projects", "elasticsearch") { Document = project }))
					.Request(c => c.Create(new CreateRequest<Project>(project, "different-projects", "elasticsearch")))
					.RequestAsync(c => c.CreateAsync(new CreateRequest<Project>(project, "different-projects", "elasticsearch")))
					.RequestAsync(c => c.CreateAsync(new CreateRequest<Project>("different-projects", "elasticsearch")
						{ Document = project }))
				;
		}
	}
}
