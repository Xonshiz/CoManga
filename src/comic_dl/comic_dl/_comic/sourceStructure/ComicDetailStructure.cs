using System.Collections.ObjectModel;

namespace comic_dl._comic.sourceStructure
{
    class ComicDetailStructure
    {
        public string comicTitle { get; set; }
        public string comicPubisher { get; set; }
        public string comicAuthors { get; set; }
        public string comicGenre { get; set; }
        public string comicPubication { get; set; }
        public string comicStatus { get; set; }
        public string comicImage { get; set; }
        public string comicDescription { get; set; }
        public ObservableCollection<ComicChapterList> comicChapterList = new ObservableCollection<ComicChapterList> { };
    }

    class ComicChapterList
    {
        public double chapterNumber { get; set; }
        public double chapterVolume { get; set; }
        public string chapterTitle { get; set; }
        public string chapterLink { get; set; }
    }
}
