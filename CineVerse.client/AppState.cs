using CineVerse.client.ApiResponses;
using CineVerse.client.Utils;

namespace CineVerse.client;

public class AppState
{
    public SectionType SectionType { get; set; }
    public List<Genre> Genres { get; set; } = new();


    //home genres pics
    public static readonly Dictionary<string, string> GenreBackgrounds = new()
    {
        ["Adventure"] = "https://image.tmdb.org/t/p/original/ueDw7djPgKPZfph0vC43aD2EMyF.jpg",
        ["Fantasy"] = "https://image.tmdb.org/t/p/original/x2RS3uTcsJJ9IfjNPcgDmukoEcQ.jpg",
        ["Animation"] = "https://image.tmdb.org/t/p/original/wXsQvli6tWqja51pYxXNG1LFIGV.jpg",
        ["Drama"] = "https://image.tmdb.org/t/p/original/kXfqcdQKsToO0OUXHcrrNCHDBzO.jpg",
        ["Horror"] = "https://image.tmdb.org/t/p/original/mmd1HnuvAzFc4iuVJcnBrhDNEKr.jpg",
        ["Action"] = "https://image.tmdb.org/t/p/original/oIwfoUFfWfESn0Y8u8jv9lc8li1.jpg",
        ["Comedy"] = "https://image.tmdb.org/t/p/original/1KgXxv6tHXOnakqYvMPvFwYKWiw.jpg",
        ["History"] = "https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg",
        ["Western"] = "https://image.tmdb.org/t/p/original/x4biAVdPVCghBlsVIzB6NmbghIz.jpg",
        ["Thriller"] = "https://image.tmdb.org/t/p/original/p1PLSI5Nw2krGxD7X4ulul1tDAk.jpg",
        ["Crime"] = "https://image.tmdb.org/t/p/original/tmU7GeKVybMWFButWEGl2M4GeiP.jpg",
        ["Documentary"] = "https://image.tmdb.org/t/p/original/yOpNvGSsT0YECuhhrlGBLIshh0u.jpg",
        ["Science Fiction"] = "https://image.tmdb.org/t/p/original/qr7dUqleMRd0VgollazbmyP9XjI.jpg",
        ["Mystery"] = "https://image.tmdb.org/t/p/original/77aHwg1SCy89rfvQtiruPU58qEV.jpg",
        ["Music"] = "https://image.tmdb.org/t/p/original/nlPCdZlHtRNcF6C9hzUH4ebmV1w.jpg",
        ["Romance"] = "https://image.tmdb.org/t/p/original/sCzcYW9h55WcesOqA12cgEr9Exw.jpg",
        ["Family"] = "https://image.tmdb.org/t/p/original/mXLVA0YL6tcXi6SJSuAh9ONXFj5.jpg",
        ["War"] = "https://image.tmdb.org/t/p/original/bdD39MpSVhKjxarTxLSfX6baoMP.jpg",
        ["TV Movie"] = "https://image.tmdb.org/t/p/original/jXZ2tyJl44yKvh22I6ooQwU5rFM.jpg"
    };

}
