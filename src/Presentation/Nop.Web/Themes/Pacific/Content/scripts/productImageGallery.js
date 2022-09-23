// jquery sampled from http://www.richardshepherd.com/how-to-use-jquery-with-a-json-flickr-feed-to-display-photos/
$(document).ready(function () {
  //$.getJSON("https://api.flickr.com/services/feeds/photos_public.gne?jsoncallback=?",
  //  {
  //    lang: "en-us",
  //    id: "43759933@N03",
  //    format: "json"
  //  }, displayImages);
  $.getJSON("/Admin/RoxyFileman/ProcessRequest?a=FILESLIST&d=testing-roxy", displayImages);
 

  //$.getJSON("https://localhost:53145/Admin/RoxyFileman/ProcessRequest?a=FILESLIST&d=/images/uploaded/3&type=image", displayImages);

  //_roxyFilemanService.GetFilesAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["type"]);
  //https://localhost:53145/Admin/RoxyFileman/ProcessRequest?a=FILESLIST&d=%2Fimages%2Fuploaded%2F3&type=image

  function displayImages(data) {
    console.log(data);
    //var limit = 4;
    var htmlString = "";

    //$.each(data.items, function (i, item) {
    //  console.log(data.items);
    //  //if(i>limit) return false;
    //  var sourceSquare = (item.media.m).replace("_m.jpg", "_s.jpg");
    //  //var sourceSquare = (item.media.m);
    //  var largeImage = (item.media.m).replace("_m.jpg", "_b.jpg");
    //  htmlString += '<a href="' + largeImage + '" ';
    //  htmlString += ' class="thumbnail col-md-1 col-sm-2 col-xs-3" '
    //  htmlString += ' data-gallery ';
    //  htmlString += ' title="' + item.title + '"';
    //  htmlString += '>';
    //  htmlString += '<img title="' + item.title + '" src="' + sourceSquare;
    //  htmlString += '" alt="';
    //  htmlString += item.title + '" />';
    //  htmlString += '</a>';
    //});
    for (i = 0; i < data.length; ++i) {
     
      var sourceSquare = data[i].p;
      var largeImage = data[i].p;
      htmlString += '<a href="' + largeImage + '" ';
      htmlString += ' class="thumbnail col-md-1 col-sm-2 col-xs-3" '
      htmlString += ' data-gallery ';
      htmlString += ' title="' + data[i].t + '"';
      htmlString += '>';
      htmlString += '<img title="' + data[i].t + '" src="' + sourceSquare;
      htmlString += '" alt="';
      htmlString += data[i].t + '" />';
      htmlString += '</a>';
    }

    $('#otembImages').html(htmlString + "");

    document.getElementById('otembImages').onclick = function registerGallery(event) {
      event = event || window.event;
      var target = event.target || event.srcElement,
        link = target.src ? target.parentNode : target,
        options = { index: link, event: event },
        links = this.getElementsByTagName('a');
      blueimp.Gallery(links, options);
    };
  }
});
