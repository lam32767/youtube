$(function () {
    init();
    $('form').on("submit", function (x) {
        x.preventDefault();

        var req = gapi.client.youtube.search.list({
            part: "snippet",
            type: "video",
              q: encodeURIComponent($("#searchstring").val()).replace(/%20/g, "+"),
            maxResults: 50
        });

        req.execute(function (resp) {
            //console.log(resp);
            //alert(resp.result.items.length);
            $("#searchresults").append(resp.result.items.length.toString()).append(" items found<br><br>");

            var emptytab = "<table id='theTable' class='display'></table>";
            $("#searchresults").append(emptytab);

            var pages = resp.result;
            var dataset = [];
            $.each(pages.items, function (index, item) {
                //console.log(item);
                dataset[index] = [
                    item.snippet.title,
                    "*****",
                    makeFaveLink(item.id.videoId),
                    "0",
                    "0",
                    item.snippet.channelTitle ,
                    "cmt",
                    item.snippet.publishedAt,
                    makeViewLink(item.id.videoId),
                    item.id.videoId
                ];
                console.log(dataset[index]);
            });

            $('#theTable').DataTable({
                "iDisplayLength": 25,
                "bJQueryUI": true,
                "aaSorting": [],
                "data": dataset,
                "sPaginationType": "full_numbers",
                columns: [
                { title: "Title" },
                { title: "Rating" },
                { title: "Favorite" },
                { title: "Likes" },
                { title: "Dislikes" },
                { title: "Channel" },
                { title: "Comments" },
                { title: "Date" },
                { title: "View"}
                ]
            });
        });
    });
});

function makeViewLink(videoId) {
    return '<a href="https://www.youtube.com/watch?v=' + videoId + '"  target="_blank" >View</a>';
}
function makeFaveLink(videoId) {
    return '<a href="/home/Details/'+videoId+'">Fave It</a>';
}

function init() {
    gapi.client.setApiKey("AIzaSyCPUBD39ZgKGAcK1s_IDYIJmMhyTU7k79Y");
    gapi.client.load("youtube", "v3", function () {});
}

