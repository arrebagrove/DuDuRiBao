﻿using Brook.DuDuRiBao.Models;
using Brook.DuDuRiBao.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brook.DuDuRiBao.ViewModels
{
    public partial class MainViewModel
    {
        public async Task RequestHotArticles(bool isLoadingMore)
        {
            if (isLoadingMore)
                return;

            var hotArticleList = await DataRequester.RequestHotArticles().ContinueWith(hotArticlesTask =>
            {
                var hotArticles = hotArticlesTask.Result;
                if (string.IsNullOrEmpty(hotArticles))
                    return null;
                return HotArticleBuilder.Builder(hotArticles);
            });

            if (hotArticleList != null)
            {
                StoryDataList.Clear();
                StoryDataList.AddRange(hotArticleList);
            }
        }
    }
}
