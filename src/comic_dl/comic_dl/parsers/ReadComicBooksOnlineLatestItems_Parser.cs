using comic_dl._comic;
using comic_dl._comic.sourceStructure;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using comic_dl.internalData;
using Microsoft.AppCenter.Crashes;
using comic_dl.commonFunctionalities;

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
            var ComicListNode = doc.GetElementbyId("Comics");
            for (int i = 0; i < ComicListNode.ChildNodes.Count; i++)
            {
                // Since the Elements are in ODD Index, we need to incriment by 1 first here for Usage.
                i++;
                int count = ComicListNode.ChildNodes.Count;
                try
                {
                    _comicList.Add(new ComicListStructure
                    {
                        comicTitle = CommonFunctions.ToUpperCase(Convert.ToString(ComicListNode.ChildNodes[i].ChildNodes[3].FirstChild.ChildNodes[0].InnerText)),
                        comicLink = ServiceUrl.base_comic_url + Convert.ToString(ComicListNode.ChildNodes[i].ChildNodes[1].FirstChild.Attributes[0].Value),
                        comicIssue = Convert.ToString(ComicListNode.ChildNodes[i].ChildNodes[5].FirstChild.ChildNodes[0].InnerText).Replace("Issue", "").Replace("#", "").Trim(),
                        comicCoverImage = ServiceUrl.base_comic_url + Convert.ToString(ComicListNode.ChildNodes[i].ChildNodes[1].ChildNodes[0].ChildNodes[0].Attributes[1].Value)
                    });
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("I right Now : " + i);
                    continue;
                }
            }

            return Task.FromResult(_comicList);
        }

        public async Task<ComicDetailStructure> GetComicDetails(string comicUrl)
        {
            ComicDetailStructure comicDetailStructure = new ComicDetailStructure();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(await LatestComicFetcher.GetComicDetails(comicUrl));
            List<string> genresList = new List<string>() { };
            List<string> publishersList = new List<string>() { };
            // Authors Removed.
            // Status Removed.
            // Publication Removed
            var _comicTitle = doc.DocumentNode.SelectSingleNode("//h1[contains(@class,'page-title')]");
            var comicCover = doc.DocumentNode.SelectSingleNode("//img[contains(@class,'mypic')]");
            var genres = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'field field-name-field-genres field-type-taxonomy-term-reference field-label-above')]");
            var publishers = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'field field-name-field-publisher field-type-taxonomy-term-reference field-label-above')]");
            var description = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'field field-name-body field-type-text-with-summary field-label-hidden')]");

            foreach (var genreElement in genres.ChildNodes[1].ChildNodes)
            {
                genresList.Add(CommonFunctions.ToUpperCase(genreElement.ChildNodes[0].FirstChild.InnerText.Trim()));
            }
            foreach (var publisherElement in publishers.ChildNodes[1].ChildNodes)
            {
                publishersList.Add(CommonFunctions.ToUpperCase(publisherElement.ChildNodes[0].FirstChild.InnerText.Trim()));
            }
            var comicInfo = doc.DocumentNode.SelectNodes("//div[contains(@id,'content-wrap')]");
            try
            {
                comicDetailStructure.comicTitle = CommonFunctions.ToUpperCase(_comicTitle.FirstChild.InnerText);
                comicDetailStructure.comicPubisher = String.Join(",", publishersList);
                comicDetailStructure.comicAuthors = "";
                comicDetailStructure.comicGenre = String.Join(",", genresList);
                comicDetailStructure.comicPubication = "";
                comicDetailStructure.comicStatus = "";
                comicDetailStructure.comicImage = ServiceUrl.base_comic_url + comicCover.Attributes[1].DeEntitizeValue;
                comicDetailStructure.comicDescription = description.FirstChild.InnerText;
            }
            catch (Exception Ex)
            {
                comicDetailStructure.comicTitle = (string.IsNullOrEmpty(comicDetailStructure.comicTitle)) ? "Title Not Found" : comicDetailStructure.comicTitle;
                comicDetailStructure.comicPubisher = (string.IsNullOrEmpty(comicDetailStructure.comicPubisher)) ? "None" : comicDetailStructure.comicPubisher;
                comicDetailStructure.comicAuthors = "";
                comicDetailStructure.comicGenre = (string.IsNullOrEmpty(comicDetailStructure.comicGenre)) ? "None" : comicDetailStructure.comicGenre;
                comicDetailStructure.comicPubication = "";
                comicDetailStructure.comicStatus = "";
                comicDetailStructure.comicImage = (string.IsNullOrEmpty(comicDetailStructure.comicImage)) ? "https://comicpunch.net/mystuff/seo-sidebar.png" : comicDetailStructure.comicImage;
                comicDetailStructure.comicDescription = (string.IsNullOrEmpty(comicDetailStructure.comicDescription)) ? "No Summary Needed" : comicDetailStructure.comicDescription;
            }
            var comicChapters = doc.DocumentNode.SelectNodes("//li[contains(@class,'chapter')]");
            foreach (var chapter in comicChapters)
            {
                if (!chapter.ChildNodes[0].InnerText.Contains("at Comicpunch.net!"))
                {
                    try
                    {
                        comicDetailStructure.comicChapterList.Add(
                        new ComicChapterList
                        {
                            chapterTitle = CommonFunctions.ToUpperCase(chapter.InnerText),
                            chapterLink = chapter.ChildNodes[0].Attributes[0].Value
                        });
                    }
                    catch (Exception Ex)
                    {
                        continue;
                    }
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

            // Currently disabling further results.
            // This needs new implementation.

            //for (int i = 1; i <= lastSearchPageNumber + 1; i++)
            //{
            //    /* If last Search Page = 1, then we do not have multiple
            //     * search pages.
            //     * So, we do not need to iterate over things. We're pretty much
            //     * done here.
            //     */               
            //    if (i != lastSearchPageNumber)
            //    {
            //        string webResponse = await LatestComicFetcher.GetComicPageContent(ServiceUrl.base_comic_search_url + searchText + "?page=" + i);
            //        doc.LoadHtml(webResponse);
            //        comicLinkNodes = doc.DocumentNode.SelectNodes("//h3[contains(@class,'title')]");
            //        await ExtractLinksFromNodes(comicLinkNodes);
            //    }
            //}

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
