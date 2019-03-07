using comic_dl._comic;
using comic_dl._comic.sourceStructure;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using comic_dl.internalData;
using Microsoft.AppCenter.Crashes;

namespace comic_dl.parsers
{
    class ReadComicBooksOnlineLatestItems_Parser
    {
        private ObservableCollection<ComicListStructure> _comicList = new ObservableCollection<ComicListStructure> { };
        ComicImagesStructure comicImagesStructure;
        private List<string> urlList = new List<string>() { };

        public Task<ObservableCollection<ComicListStructure>> ExtractLatestComics(string source)
        {
            ComicListStructure comicListStructure = new ComicListStructure();
            var doc = new HtmlDocument();
            doc.LoadHtml(source);
            var comicInfo = doc.DocumentNode.SelectNodes("//div[contains(@class,'popbox')]");

            foreach (var item in comicInfo)
            {
                // Popular titles show also the Manga. So, we need to remove those.
                if (item.ChildNodes[3].ChildNodes[0].Attributes[0].Value.Contains("/manga/"))
                    continue;

                try
                {
                    _comicList.Add(new ComicListStructure
                    {
                        comicTitle = Convert.ToString(item.ChildNodes[1].InnerText),
                        comicLink = ServiceUrl.base_comic_url + Convert.ToString(item.ChildNodes[3].ChildNodes[0].Attributes[0].Value),
                        comicIssue = Convert.ToString(item.ChildNodes[5].InnerText).Replace("Issue", "").Replace("#", "").Trim(),
                        comicCoverImage = ServiceUrl.base_comic_url + Convert.ToString(item.ChildNodes[3].FirstChild.FirstChild.EndNode.Attributes[1].Value)
                    });
                }
                catch (Exception)
                {
                    _comicList.Add(new ComicListStructure
                    {
                        comicTitle = Convert.ToString(item.ChildNodes[1].InnerText),
                        comicLink = ServiceUrl.base_comic_url + Convert.ToString(item.ChildNodes[3].ChildNodes[0].Attributes[0].Value),
                        comicIssue = Convert.ToString(item.ChildNodes[5].InnerText).Replace("Issue", "").Replace("#", "").Trim(),
                        comicCoverImage = ServiceUrl.base_comic_url + Convert.ToString(item.ChildNodes[3].FirstChild.ChildNodes[1].Attributes[1].Value)
                    }
                );
                }

            }

            return Task.FromResult(_comicList);
        }

        public async Task<ComicDetailStructure> GetComicDetails(string comicUrl)
        {
            ComicDetailStructure comicDetailStructure = new ComicDetailStructure();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(await LatestComicFetcher.GetComicDetails(comicUrl));
            var comicInfo = doc.DocumentNode.SelectNodes("//li[contains(@class,'info')]");
            try
            {
                comicDetailStructure.comicTitle = comicInfo[0].ChildNodes[1].InnerText;
                comicDetailStructure.comicPubisher = comicInfo[1].ChildNodes[1].InnerText;
                comicDetailStructure.comicAuthors = comicInfo[3].ChildNodes[1].InnerText;
                comicDetailStructure.comicGenre = comicInfo[4].ChildNodes[1].InnerText;
                comicDetailStructure.comicPubication = comicInfo[5].ChildNodes[1].InnerText;
                comicDetailStructure.comicStatus = comicInfo[6].ChildNodes[1].InnerText;
                comicDetailStructure.comicImage = ServiceUrl.base_comic_url + doc.DocumentNode.SelectNodes("//img[contains(@class,'series')]")[0].Attributes["src"].Value;
                comicDetailStructure.comicDescription = doc.DocumentNode.SelectNodes("//li[contains(@class,'summary')]")[0].ChildNodes[1].InnerText;
            }
            catch (Exception)
            {
                comicDetailStructure.comicAuthors = "N/A";
                comicDetailStructure.comicPubication = "N/A";
                comicDetailStructure.comicStatus = "N/A";

                var comicTitle = doc.DocumentNode.SelectSingleNode("//h1[contains(@class,'page-title')]");
                comicDetailStructure.comicTitle = comicTitle.InnerText;

                var comicImage = doc.DocumentNode.SelectSingleNode("//img[contains(@class,'mypic')]");
                comicDetailStructure.comicImage = ServiceUrl.base_comic_url + comicImage.Attributes["src"].Value;

                var comicPubisher = doc.DocumentNode.SelectNodes("//div[contains(@class,'field field-name-field-publisher field-type-taxonomy-term-reference field-label-inline clearfix')]");
                comicDetailStructure.comicPubisher = comicPubisher[0].ChildNodes[1].InnerText;

                var comicGenre = doc.DocumentNode.SelectNodes("//div[contains(@class,'field field-name-field-genres field-type-taxonomy-term-reference field-label-inline clearfix')]");
                comicDetailStructure.comicGenre = comicGenre[0].ChildNodes[1].InnerText;

                var comicDescription = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'field field-name-field-synopsis field-type-text-with-summary field-label-hidden')]");
                comicDetailStructure.comicDescription = comicDescription.InnerText;
            }
            var comicChapters = doc.DocumentNode.SelectNodes("//li[contains(@class,'chapter')]");
            foreach (var chapter in comicChapters)
            {
                try
                {
                    comicDetailStructure.comicChapterList.Add(
                    new ComicChapterList
                    {
                        chapterTitle = chapter.InnerText,
                        chapterLink = chapter.ChildNodes[0].Attributes[0].Value
                    });
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return comicDetailStructure;
        }

        public async Task<ComicImagesStructure> GetComicChapterImages(string comicChapterUrl)
        {
            comicImagesStructure = new ComicImagesStructure();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(await LatestComicFetcher.GetComicChapterDetails(comicChapterUrl + "?q=fullchapter"));
            string chapterNumber = doc.DocumentNode.SelectNodes("//option[contains(@selected,'selected')]")[0].InnerText;
            comicImagesStructure.comicChapterNumber = chapterNumber.Split()[chapterNumber.Split().Length - 1];
            var comicInfo = doc.DocumentNode.SelectNodes("//img[contains(@class,'picture')]");
            
            if (comicInfo != null)
            {
                foreach (HtmlNode link in comicInfo)
                {
                    try
                    {
                        comicImagesStructure.comicChapterImageLink.Add(ServiceUrl.base_comic_image_url + link.Attributes["src"].Value.Replace(" ", "%20"));
                    }
                    catch (Exception NoURLException)
                    {
                        Console.WriteLine("No Image URL Found : " + NoURLException);
                        continue;
                    }
                }
            }
            
            return comicImagesStructure;
        }

        private Task<bool> ExtractLinksFromNodes(HtmlNodeCollection nodeList)
        {
            foreach (var item in nodeList)
            {
                string item_new = item.SelectSingleNode(".//a").Attributes["href"].Value;
                if (!string.IsNullOrWhiteSpace(item_new))
                {
                    urlList.Add(item_new);
                }
            }
            return Task.FromResult(true);
        }

        public async Task<ObservableCollection<ComicListStructure>> ExtractComicSearch(string searchText, string source)
        {
            ComicListStructure comicListStructure = new ComicListStructure();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);
            var comicLinkNodes = doc.DocumentNode.SelectNodes("//h3[contains(@class,'title')]");
            var comicLastSearchNode = doc.DocumentNode.SelectSingleNode("//a[contains(@title,'Go to last page')]");
            int lastSearchPageNumber = Convert.ToInt16(comicLastSearchNode.Attributes["href"].Value.Split('=')[1].Trim());
            await ExtractLinksFromNodes(comicLinkNodes);

            for (int i = 1; i <= lastSearchPageNumber + 1; i++)
            {
                /* If last Search Page = 1, then we do not have multiple
                 * search pages.
                 * So, we do not need to iterate over things. We're pretty much
                 * done here.
                 */               
                if (i != lastSearchPageNumber)
                {
                    string webResponse = await LatestComicFetcher.GetComicPageContent(ServiceUrl.base_comic_search_url + searchText + "?page=" + i);
                    doc.LoadHtml(webResponse);
                    comicLinkNodes = doc.DocumentNode.SelectNodes("//h3[contains(@class,'title')]");
                    await ExtractLinksFromNodes(comicLinkNodes);
                }
            }

            _comicList.Clear(); // Clear any previous data.
            ComicDetailStructure comicDetailStructure_new = new ComicDetailStructure();
            foreach (var searchUrl in urlList)
            {
                try
                {
                    comicDetailStructure_new = await GetComicDetails(searchUrl);

                    _comicList.Add(new ComicListStructure
                    {
                        comicTitle = comicDetailStructure_new.comicTitle,
                        comicLink = searchUrl,
                        comicIssue = comicDetailStructure_new.comicDescription,
                        comicCoverImage = ServiceUrl.base_comic_url + comicDetailStructure_new.comicImage
                    }
                );
                }
                catch (Exception ComicExtractionException)
                {
                    Crashes.TrackError(ComicExtractionException);
                    continue;
                }
            }

            return _comicList;
        }
    }
}
