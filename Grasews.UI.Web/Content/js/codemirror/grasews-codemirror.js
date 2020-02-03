var codeMirrorEditor;

$(window).resize(function () {
    resizeCodeMirrorContainer();
});

$('.nav-tabs a').on('shown.bs.tab', function () {
    if ($(event.target).context.hash === "#tab_code") {
        codeMirrorEditor.refresh();
    }
});

function initCodeMirrorEditor() {
    if (!(codeMirrorEditor instanceof CodeMirror)) {
        // Load main editor
        var el = document.getElementById("txtcode");
        codeMirrorEditor = CodeMirror.fromTextArea(el, {
            lineNumbers: true,
            lineWrapping: true,
            viewportMargin: Infinity,
            readOnly: true,
            styleSelectedText: true
        });
        //mainEditor.setSize('100%', 'inherit');
    }

    resizeCodeMirrorContainer();
    codeMirrorEditor.refresh();
}

function resizeCodeMirrorContainer() {
    var windowHeight = $(window).height();
    var headerBarHeight = $(".main-header").height();
    var navTabsHeight = $(".nav.nav-tabs").height();
    var footerBarHeight = $(".main-footer").height();
    var contentWrapperWidth = $(".content-wrapper").width();

    $(".CodeMirror.cm-s-default").height(windowHeight - headerBarHeight - navTabsHeight - footerBarHeight - 40);

    //$(".CodeMirror.cm-s-default").width(contentWrapperWidth - 2);
}

function fillCodeMirror(content) {
    codeMirrorEditor.setValue(content);
    codeMirrorEditor.clearHistory();

    //codeMirrorEditor.markText({ line: 6, ch: 3 }, { line: 6, ch: 10 }, { className: "codeMirror-selected-text" });

    setTimeout(function () {
        codeMirrorEditor.refresh();
    }, 1000);

    //markCodeMirrorModelReferences(content);
}

function markCodeMirrorModelReferences(content) {

    var modelReferenceText = 'modelReference';
    //var regex = new RegExp(modelReferenceText, 'gi');

    //var results = new Array();

    //while (regex.exec(content)) {
    //    var modelReferenceStart = regex.lastIndex;

    //    var modelReferenceEnd = modelReferenceStart + "modelReference".length;

    //    var initialPosition = codeMirrorEditor.posFromIndex(modelReferenceStart);
    //    var finalPosition = codeMirrorEditor.posFromIndex(modelReferenceEnd);

    //    markCodeMirrorText(initialPosition, finalPosition);
    //}



    var indices = getIndicesOf(modelReferenceText, content);

    for (var i = 0; i < indices.length; i++) {
        var modelReferenceStart = indices[i];

        var modelReferenceEnd = modelReferenceStart + modelReferenceText.length;

        var initialPosition = codeMirrorEditor.posFromIndex(modelReferenceStart);
        var finalPosition = codeMirrorEditor.posFromIndex(modelReferenceEnd);

        markCodeMirrorText(initialPosition, finalPosition);
    }
}

function markCodeMirrorText(initialPosition, finalPosition) {
    codeMirrorEditor.markText(initialPosition, finalPosition, { className: "codeMirror-selected-text" });
}

function getIndicesOf(searchStr, str, caseSensitive) {
    var searchStrLen = searchStr.length;
    if (searchStrLen === 0) {
        return [];
    }
    var startIndex = 0, index, indices = [];
    if (!caseSensitive) {
        str = str.toLowerCase();
        searchStr = searchStr.toLowerCase();
    }
    while ((index = str.indexOf(searchStr, startIndex)) > -1) {
        indices.push(index);
        startIndex = index + searchStrLen;
    }
    return indices;
}