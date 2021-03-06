﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.GetWatch
{
	public class GetWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_watcher/watch/watch_id")
			.Fluent(c => c.GetWatch("watch_id"))
			.Request(c => c.GetWatch(new GetWatchRequest("watch_id")))
			.FluentAsync(c => c.GetWatchAsync("watch_id"))
			.RequestAsync(c => c.GetWatchAsync(new GetWatchRequest("watch_id")));
	}
}
