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
                    index + 1,
                    item.snippet.title,
                    "*****",
                    makeFaveLinkFromItem(item, item.snippet.title, item.snippet.channelTitle, item.snippet.publishedAt),
                    "0",
                    "0",
                    item.snippet.channelTitle,
                    "cmt",
                    item.snippet.publishedAt,
                    makeViewLink(item.id.videoId),
                    item.id.videoId
                ];
                console.log(makeFaveLinkFromItem(item));
            });

            $('#theTable').DataTable({
                "iDisplayLength": 10,
                "bJQueryUI": true,
                "aaSorting": [],
                "data": dataset,
                "sPaginationType": "full_numbers",
                columns: [
                { title: "Num" },
                { title: "Title" },
                { title: "Rating" },
                { title: "Favorite" },
                { title: "Likes" },
                { title: "Dislikes" },
                { title: "Channel" },
                { title: "Comments" },
                { title: "Date" },
                { title: "View" }
                ]
            });
        });
    });
});

function makeViewLink(videoId) {
    return '<a href="https://www.youtube.com/watch?v=' + videoId + '"  target="_blank" >View</a>';
}

function makeFaveLink(videoId) {
    return '<a href="/home/Adder/' + videoId + '">Fave It</a>';
}

function makeFaveLinkFromItem(item, title, channelTitle, publishedAt) {
    var sender = new Object();

    sender.Id = item.id.videoId;

    if (title === undefined)
        sender.Title = ' ';
    else
        sender.Title = title;

    if (channelTitle === undefined)
        sender.ChannelTitle = ' ';
    else
        sender.ChannelTitle = channelTitle;
    sender.Rating = '*****';
    sender.Comment = "comment";

    if (publishedAt === undefined)
        sender.PublishDate = new Date();
    else
        sender.PublishDate = publishedAt;

    sender.Likes = 120;
    sender.Dislikes = 200;

    var senderstring = JSON.stringify(sender);
    senderstring = senderstring.replace(/"/g, '~');
    var retval = '<a href="/home/Adder/?sender=' + senderstring + '">Fave It</a>';
    console.log('Retval ' + retval);
    return retval;
}

function init() {
    gapi.client.setApiKey("AIzaSyCPUBD39ZgKGAcK1s_IDYIJmMhyTU7k79Y");
    gapi.client.load("youtube", "v3", function () { });
}

