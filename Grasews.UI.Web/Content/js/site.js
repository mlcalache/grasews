/******************************************************************************************/
// Global variables - used in other files
var GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE = "GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE";
var GRASEWS_ONTOLOGIES_OPENED_COOKIE = "GRASEWS_ONTOLOGIES_OPENED_COOKIE";
var GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE = "GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE";
var listOfModelReferencesRemoved = [];
var showAjaxLoading = true;
/******************************************************************************************/

var notifyServiceDescriptionUpdateEvent = new Event('notifyServiceDescriptionUpdateEvent');
var notifyTaskCreatedEvent = new Event('notifyTaskCreatedEvent');
var notifyIssueCreatedEvent = new Event('notifyIssueCreatedEvent');
var notifyTaskCommentCreatedEvent = new Event('notifyTaskCommentCreatedEvent');
var notifyIssueAnswerCreatedEvent = new Event('notifyIssueAnswerCreatedEvent');

/* ---------------------- AJAX UX : BEGIN ---------------------- */

$(document).ajaxStart(function () {
    startAjaxLoading();
});

$(document).ajaxStop(function () {
    stopAjaxLoading();
});

function startAjaxLoading() {
    if (showAjaxLoading) {
        $(".loading-background").fadeIn().css("display", "flex");
    }
}

function stopAjaxLoading() {
    $(".loading-background").fadeOut();
}

/* ---------------------- AJAX UX : END ---------------------- */

/* ---------------------- UI Controls : BEGIN  ---------------------- */

$(function () {
    $("#modal-launcher, #modal-background, #modal-close").click(function () {
        $("#modal-content,#modal-background").toggleClass("active");
    });

    $('[data-toggle="tooltip"]').tooltip({ container: 'body' });

    load_select2ToHtml();

    init_ButtonCheckbox();

    $("#select-ontology").change(function () {
    });

    $("#lnk-save").click(function () {
        updateServiceDescription();
    });

    $("#lnk-export-image").click(function () {
        exportImage();
    });

    $("#lnk-open-share").click(function () {
        modalShareServiceDescription();
    });

    $("#lnk-refresh-service-description").click(function () {
        refreshServiceDescriptionOnView();
        blockEditing(false);
    });

    $("#lnk-open-service-description").click(function () {
        modalOpenServiceDescription();
    });

    $("#lnk-open-ontology").click(function () {
        modalOpenOntology();
    });

    $("#lnk-open-download").click(function () {
        //TODO : Add event to download wsdl.
    });

    $("#lnk-open-create-issue").click(function () {
        modalAddIssue();
    });

    $("#lnk-open-issues-list").click(function () {
        modalIssueList();
    });

    $("#lnk-open-create-task").click(function () {
        modalAddTask();
    });

    $("#lnk-open-tasks-list").click(function () {
        modalTaskList();
    });

    getServiceDescriptionsAlreadyOpened();

    getOntologiesAlreadyOpened();

    initCodeMirrorEditor();

    // SignalR events: Begin
    $.connection.serviceDescriptionHub.client.broadcastNotificationServiceDescriptionUpdate = function (idServiceDescription, message) {
        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
        if (parseInt(idServiceDescriptionOnView) === idServiceDescription) {
            refreshServiceDescriptionOnView();
            //addToastMessage(message, "signalr", 0);
            //blockEditing(true);
        }
    };

    $.connection.serviceDescriptionHub.client.broadcastNotificationTaskCreated = function (idServiceDescription, message) {
        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
        if (parseInt(idServiceDescriptionOnView) === idServiceDescription) {
            addToastMessage(message, "success");
        }
    };

    $.connection.serviceDescriptionHub.client.broadcastNotificationTaskCommentCreated = function (idServiceDescription, message) {
        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
        if (parseInt(idServiceDescriptionOnView) === idServiceDescription) {
            addToastMessage(message, "success");
        }
    };

    $.connection.serviceDescriptionHub.client.broadcastNotificationIssueCreated = function (idServiceDescription, message) {
        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
        if (parseInt(idServiceDescriptionOnView) === idServiceDescription) {
            addToastMessage(message, "success");
        }
    };

    $.connection.serviceDescriptionHub.client.broadcastNotificationIssueAnswerCreated = function (idServiceDescription, message) {
        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
        if (parseInt(idServiceDescriptionOnView) === idServiceDescription) {
            addToastMessage(message, "success");
        }
    };

    $.connection.hub.start().done(function () {
        window.addEventListener('notifyServiceDescriptionUpdateEvent', function (e) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            $.connection.serviceDescriptionHub.server.notifyServiceDescriptionUpdate(idServiceDescriptionOnView);
        }, false);

        window.addEventListener('notifyTaskCreatedEvent', function (e) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            $.connection.serviceDescriptionHub.server.notifyTaskCreated(idServiceDescriptionOnView);
        }, false);

        window.addEventListener('notifyTaskCommentCreatedEvent', function (e) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            $.connection.serviceDescriptionHub.server.notifyTaskCommentCreated(idServiceDescriptionOnView);
        }, false);

        window.addEventListener('notifyIssueCreatedEvent', function (e) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            $.connection.serviceDescriptionHub.server.notifyIssueCreated(idServiceDescriptionOnView);
        }, false);

        window.addEventListener('notifyIssueAnswerCreatedEvent', function (e) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            $.connection.serviceDescriptionHub.server.notifyIssueAnswerCreated(idServiceDescriptionOnView);
        }, false);
    });
    // SignalR events : End

    $(window).bind('keydown', function (event) {
        //console.log('--> ' + String.fromCharCode(event.which).toLowerCase());
        if (event.ctrlKey || event.metaKey) {
            switch (String.fromCharCode(event.which).toLowerCase()) {
                case 's':
                    event.preventDefault();
                    updateServiceDescription();
                    break;
                case 'i':
                    event.preventDefault();
                    modalAddIssue();
                    break;
                case '¿':
                    event.preventDefault();
                    modalIssueList();
                    break;
                case 'm':
                    event.preventDefault();
                    modalAddTask();
                    break;
                case 'þ':
                    event.preventDefault();
                    modalTaskList();
                    break;
                //case 'f':
                //    event.preventDefault();
                //    alert('ctrl-f');
                //    break;
                //case 'g':
                //    event.preventDefault();
                //    alert('ctrl-g');
                //    break;
            }
        }
    });
});

function exportImage() {
    const options =
    {
        "bg": "#605ca8",
        "full": false
    };

    var png64 = cy.png(options);

    $("#modal-view-image").modal("toggle");

    // put the png data in an img tag
    $('#img-graph').attr('src', png64);
}

function load_select2ToHtml() {
    //$(".select2").select2();

    $(".select2").select2({
        escapeMarkup: function (m) { return m; } //to use the &nbsp;
    });

    $(".select2-multiple").select2({
        allowClear: true,
        tags: true
    });
}

function load_iCheckToHtml() {
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-purple',
        radioClass: 'iradio_square-purple',
        increaseArea: '20%' // optional
    });
}

function init_ButtonCheckbox() {
    $('.button-checkbox').each(function () {
        // Settings
        var $widget = $(this),
            $button = $widget.find('button'),
            $checkbox = $widget.find('input:checkbox'),
            color = $button.data('color'),
            settings = {
                on: {
                    icon: 'glyphicon glyphicon-check'
                },
                off: {
                    icon: 'glyphicon glyphicon-unchecked'
                }
            };

        // Event Handlers
        $button.on('click', function () {
            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay();
        });
        $checkbox.on('change', function () {
            updateDisplay();
        });

        // Actions
        function updateDisplay() {
            var isChecked = $checkbox.is(':checked');

            // Set the button's state
            $button.data('state', (isChecked) ? "on" : "off");

            // Set the button's icon
            $button.find('.state-icon')
                .removeClass()
                .addClass('state-icon ' + settings[$button.data('state')].icon);

            // Update the button's color
            if (isChecked) {
                $button
                    .removeClass('btn-default')
                    .addClass('btn-' + color + ' active');
            }
            else {
                $button
                    .removeClass('btn-' + color + ' active')
                    .addClass('btn-default');
            }
        }

        // Initialization
        function init() {
            updateDisplay();

            // Inject the icon if applicable
            if ($button.find('.state-icon').length === 0) {
                $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
            }
        }
        init();
    });
}

function activateTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
}

function blockEditing(block) {
    var $lnk_save = $("#lnk-save");
    var $div_alert_you_need_to_refresh = $("#div-alert-you-need-to-refresh");
    var $lnk_open_download = $("#lnk-open-download");

    if (block) {
        $div_alert_you_need_to_refresh.show();

        $lnk_save.fadeTo("slow", 0.20);
        $lnk_save.css('cursor', 'default');
        $lnk_save.removeClass("faa-parent animated-hover");
        $lnk_save.unbind("click");
        $lnk_save.click(function () {
            addToastMessage("You need to refresh the service description to continue editing it.", "warning");
        });

        $lnk_open_download.fadeTo("slow", 0.20);
        $lnk_open_download.css('cursor', 'default');
        $lnk_open_download.removeClass("faa-parent animated-hover");
        $lnk_open_download.unbind("click");
        $lnk_open_download.click(function () {
            addToastMessage("You need to refresh the service description to continue editing it.", "warning");
        });
    }
    else {
        $div_alert_you_need_to_refresh.hide();

        $lnk_save.fadeTo("slow", 1);
        $lnk_save.css('cursor', 'pointer');
        $lnk_save.addClass("faa-parent animated-hover");
        $lnk_save.unbind("click");
        $lnk_save.click(function () {
            updateServiceDescription();
        });

        $lnk_open_download.fadeTo("slow", 1);
        $lnk_open_download.css('cursor', 'pointer');
        $lnk_open_download.addClass("faa-parent animated-hover");
        $lnk_open_download.unbind("click");
        $lnk_open_download.click(function () {
            //TODO : Add event to download wsdl.
        });

        $(".toast-signalr").remove();
    }
}

/* ---------------------- UI Controls : END ---------------------- */

/* ---------------------- Toast Message : BEGIN ---------------------- */

function addToastMessage(message, toastType, timeout) {
    var toastrTimeOut = 5000; //by default, set the timeout to 5 seconds

    if (timeout !== null && timeout !== undefined) {
        toastrTimeOut = timeout;
    }
    else if (toastType === "danger") {
        toastrTimeOut = 20000; //if it is a danger message, set timeout to 20 seconds
    }

    var optionsOverride = { "closeButton": true, "newestOnTop": true, "timeOut": toastrTimeOut, "escapeHtml": false };

    toastr[toastType](message, '', optionsOverride);
}

/* ---------------------- Toast Message : END ---------------------- */

/* ---------------------- Cookies : BEGIN  ---------------------- */

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

/* ---------------------- Cookies : END ---------------------- */

/* ---------------------- Service Descriptions : BEGIN  ---------------------- */

function hideAllServiceDescriptionsButThis(idServiceDescription) {
    if (idServiceDescription) {
        $.each($(".service-description-eye"), function (id, element) {
            var $element = $(element);
            if ($($element.closest("li")).attr("id-service-description") !== idServiceDescription.toString()) {
                $element.removeClass("fa-eye");
                $element.addClass("fa-eye-slash");

                $element.removeClass("text-green");
                $element.addClass("text-red");
            }
            else {
                $element.addClass("fa-eye");
                $element.removeClass("fa-eye-slash");

                $element.addClass("text-green");
                $element.removeClass("text-red");
            }
        });
    }
}

function openSavedServiceDescription(idServiceDescription) {
    jQuery.support.cors = true;

    $.ajax({
        url: "Site/OpenSavedServiceDescription",
        data: { "idServiceDescription": idServiceDescription },
        type: "GET",
        success: function (openResponse) {
            if (openResponse !== null) {
                if (openResponse.serviceDescription !== null) {
                    fillCytoscape(JSON.parse(openResponse.serviceDescription.GraphJson));

                    adjustUserNodesPositionsOnGraph(JSON.parse(openResponse.serviceDescription.GraphJson));

                    fillCodeMirror(openResponse.serviceDescription.Xml);

                    //TODO
                    //fillWebServiceElementsDropDownOnAddModalFromOntology(createElementsAsyncWebServiceMethodResult.wsdlDocument)

                    $("#div-service-description-in-view").show();

                    $("#lbl-service-description-name-in-view").text(openResponse.serviceDescription.ServiceName);

                    $("#treeview-service-description").append(openResponse.serviceDescription.HtmlTreeViewMenu);

                    setCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, openResponse.serviceDescription.Id);

                    createContextMenusForTreeView();

                    hideAllServiceDescriptionsButThis(openResponse.serviceDescription.Id);

                    var message = "Service description " + openResponse.serviceDescription.ServiceName + " loaded into Grasews.";

                    console.log(message);

                    addToastMessage(message, "success");
                }
                else {
                    addToastMessage(openResponse.message, "warning");
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function uploadServiceDescription(arrayBuffer, filename) {
    var array = new Uint8Array(arrayBuffer);

    var stringByteArray = btoa(new Uint8Array(array).reduce(function (data, byte) {
        return data + String.fromCharCode(byte);
    }, ''));

    $.ajax({
        url: "Upload/UploadServiceDescriptionFile",
        dataType: "json",
        data: {
            "stringByteArray": stringByteArray,
            "filename": filename
        },
        type: "POST",
        success: function (uploadServiceDescriptionResponse) {
            if (uploadServiceDescriptionResponse !== null) {
                if (uploadServiceDescriptionResponse.serviceDescription !== null) {
                    fillCytoscape(JSON.parse(uploadServiceDescriptionResponse.serviceDescription.GraphJson));

                    fillCodeMirror(uploadServiceDescriptionResponse.serviceDescription.Xml);

                    //TODO
                    //fillWebServiceElementsDropDownOnAddModalFromOntology(createElementsAsyncWebServiceMethodResult.wsdlDocument)

                    $("#div-service-description-in-view").show();

                    $("#lbl-service-description-name-in-view").text(uploadServiceDescriptionResponse.serviceDescription.ServiceName);

                    $("#treeview-service-description").append(uploadServiceDescriptionResponse.serviceDescription.HtmlTreeViewMenu);

                    setCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, uploadServiceDescriptionResponse.serviceDescription.Id);

                    createContextMenusForTreeView();

                    hideAllServiceDescriptionsButThis(uploadServiceDescriptionResponse.serviceDescription.Id);

                    var message = "Service description " + uploadServiceDescriptionResponse.serviceDescription.ServiceName + " loaded into Grasews.";

                    console.log(message);

                    addToastMessage(message, "success");
                }
                else {
                    addToastMessage(uploadServiceDescriptionResponse.message, "warning");
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function modalOpenServiceDescription() {
    $.ajax({
        url: "Site/_ModalOpenServiceDescription",
        type: "GET",
        cache: false
    }).done(function (result) {
        $("#place-holder-partial-views").html(result);
        $('#modal-open-service-description').modal('toggle');
        load_select2ToHtml();

        $('#modal-open-service-description').on('shown.bs.modal', function () {
            $("#modal-open-service-description").find("#fileOpenWsdl").trigger('focus');
        });

        $("#nav-tab-open-new-service-description").on('shown.bs.tab', function (e) {
            $("#modal-open-service-description").find("#fileOpenWsdl").trigger('focus');
        });

        $("#nav-tab-open-saved-service-description").on('shown.bs.tab', function (e) {
            $("#modal-open-service-description").find(".select2").trigger('focus');
        });

        $("#lnkOpenSavedServiceDescription").on("click", function () {
            var idServiceDescription = $("#ddl-saved-services-descriptions").val();
            openSavedServiceDescription(idServiceDescription);
        });

        $("#lnkUploadServiceDescription").on("click", function () {
            var file = document.getElementById("fileOpenWsdl").files[0];
            var filename = file.name.replace(/\.[^/.]+$/, "");
            var reader = new FileReader();

            reader.onload = function () {
                uploadServiceDescription(this.result, filename);
                $("#fileOpenWsdl").val("");
            };

            reader.readAsArrayBuffer(file);
        });
    });
}

function getServiceDescriptionsAlreadyOpened() {
    $.ajax({
        url: "Site/GetServiceDescriptionsAlreadyOpened",
        type: "GET",
        success: function (serviceDescriptions) {
            if (serviceDescriptions !== null && serviceDescriptions.length > 0) {
                var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

                $.each(serviceDescriptions, function (index, serviceDescription) {
                    if (serviceDescription.Id.toString() === idServiceDescriptionOnView) {
                        fillCytoscape(JSON.parse(serviceDescription.GraphJson));

                        adjustUserNodesPositionsOnGraph(JSON.parse(serviceDescription.GraphJson));

                        fillCodeMirror(serviceDescription.Xml);

                        //TODO
                        //fillWebServiceElementsDropDownOnAddModalFromOntology(createElementsAsyncWebServiceMethodResult.wsdlDocument)

                        $("#div-service-description-in-view").show();

                        $("#lbl-service-description-name-in-view").text(serviceDescription.ServiceName);

                        setCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, idServiceDescriptionOnView);
                    }

                    $("#treeview-service-description").append(serviceDescription.HtmlTreeViewMenu);

                    hideAllServiceDescriptionsButThis(idServiceDescriptionOnView);

                    createContextMenusForTreeView();

                    var message = "Service description '" + serviceDescription.ServiceName + "' loaded into Grasews from previous session.";

                    console.log(message);

                    addToastMessage(message, "success");
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function closeServiceDescription(idServiceDescription, callback, callbackParameters) {
    var serviceDescriptionsCookie = getCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE);

    if (serviceDescriptionsCookie !== null) {
        var serviceDescriptionsIdsOpened = serviceDescriptionsCookie.split(",");
        var idServiceDescriptionsToKeepOpened = [];

        $.each(serviceDescriptionsIdsOpened, function (index, idServiceDescriptionOpened) {
            if (idServiceDescriptionOpened !== idServiceDescription) {
                idServiceDescriptionsToKeepOpened.push(idServiceDescriptionOpened);
            }
        });

        var newServiceDescriptionsCookie = idServiceDescriptionsToKeepOpened.join();

        if (newServiceDescriptionsCookie !== "") {
            setCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE, newServiceDescriptionsCookie);
        }
        else {
            eraseCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE);
        }

        $("#li-service-description-" + idServiceDescription).remove();

        var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

        if (idServiceDescription === idServiceDescriptionOnView) {
            if (idServiceDescriptionsToKeepOpened.length === 0) {
                clearCytoscape();

                $("#zoomValue").text("");

                $("#div-service-description-in-view").hide();
                $("#lbl-service-description-name-in-view").text("");

                fillCodeMirror("");

                eraseCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            }
            else if (idServiceDescriptionsToKeepOpened.length === 1) {
                viewServiceDescription(idServiceDescriptionsToKeepOpened[0]);

                setCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, idServiceDescriptionsToKeepOpened[0]);
            }
        }
    }
}

function viewServiceDescription(idServiceDescription) {
    jQuery.support.cors = true;

    setCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, idServiceDescription);

    $.ajax({
        url: "Site/ViewOpenedServiceDescription",
        data: { "idServiceDescription": idServiceDescription },
        type: "GET",
        success: function (viewResponse) {
            if (viewResponse !== null) {
                if (viewResponse.serviceDescription !== null) {
                    fillCytoscape(JSON.parse(viewResponse.serviceDescription.GraphJson));

                    adjustUserNodesPositionsOnGraph(JSON.parse(viewResponse.serviceDescription.GraphJson));

                    fillCodeMirror(viewResponse.serviceDescription.Xml);

                    //TODO
                    //fillWebServiceElementsDropDownOnAddModalFromOntology(createElementsAsyncWebServiceMethodResult.wsdlDocument)

                    $("#div-service-description-in-view").show();

                    $("#lbl-service-description-name-in-view").text(viewResponse.serviceDescription.ServiceName);

                    hideAllServiceDescriptionsButThis(viewResponse.serviceDescription.Id);

                    var message = "Service description '" + viewResponse.serviceDescription.ServiceName + "' on screen.";

                    console.log(message);

                    addToastMessage(message, "success");
                }
                else {
                    addToastMessage(viewResponse.message, "warning");
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function updateServiceDescription() {
    var graphJson = cy.json();
    var graphJsonStringfied = JSON.stringify(graphJson.elements);

    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        var idServiceDescription = parseInt(idServiceDescriptionOnView);
        $.ajax({
            url: "Site/UpdateServiceDescription",
            dataType: "json",
            data: {
                "idServiceDescription": idServiceDescription,
                "graphJson": graphJsonStringfied
            },
            type: "POST",
            success: function (result) {
                if (result.affectedRegisters > 0) {
                    addToastMessage(result.message, "success");
                }
                else {
                    addToastMessage(result.message, "danger");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
            }
        });
    }
}

function refreshServiceDescriptionOnView() {
    showAjaxLoading = false;
    $.ajax({
        url: "Site/RefreshServiceDescriptionOnView",
        type: "GET",
        success: function (serviceDescription) {
            if (serviceDescription !== null) {
                fillCytoscape(JSON.parse(serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(serviceDescription.GraphJson));

                fillCodeMirror(serviceDescription.Xml);

                //TODO
                //fillWebServiceElementsDropDownOnAddModalFromOntology(createElementsAsyncWebServiceMethodResult.wsdlDocument)

                $("#div-service-description-in-view").show();

                $("#lbl-service-description-name-in-view").text(serviceDescription.ServiceName);

                var liToReplace = $("#treeview-service-description").find("#li-service-description-" + serviceDescription.Id);

                var message;

                if (liToReplace.length > 0) {
                    liToReplace[0].innerHTML = $(serviceDescription.HtmlTreeViewMenu)[0].innerHTML;

                    hideAllServiceDescriptionsButThis(serviceDescription.Id);

                    createContextMenusForTreeView();

                    message = "Service description '" + serviceDescription.ServiceName + "' has been refreshed.";

                    addToastMessage(message, "success");
                }
                else {
                    message = "The requested service description has not been found in the tree view menu.";

                    addToastMessage(message, "danger");
                }
                console.log(message);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        },
        complete: function () {
            showAjaxLoading = true;
        }
    });
}

/* ---------------------- Service Description : END ---------------------- */

/* ---------------------- Ontologies : BEGIN  ---------------------- */

function hideOntology(idOntology) {
    $.each($(".ontology-eye"), function (id, element) {
        var $element = $(element);
        if ($element.attr("ontology-id") === idOntology) {
            $element.addClass("fa-eye");
            $element.removeClass("fa-eye-slash");

            $element.addClass("text-green");
            $element.removeClass("text-red");
        }
    });
}

function openSavedOntology(idOntology) {
    jQuery.support.cors = true;

    $.ajax({
        url: "Site/OpenSavedOntology",
        data: { "idOntology": idOntology },
        type: "GET",
        success: function (openResponse) {
            if (openResponse !== null) {
                if (openResponse.ontology !== undefined && openResponse.ontology !== null) {
                    //fillOntologiesDropdownMenu(createElementsAsyncWebServiceMethodResult.wsdlDocument);

                    $("#treeview-ontology").append(openResponse.ontology.HtmlTreeViewMenu);

                    createContextMenusForTreeView();

                    var message = "Ontology " + openResponse.ontology.OntologyName + " loaded into Grasews.";

                    console.log(message);

                    addToastMessage(message, "success");
                }
                else {
                    addToastMessage(openResponse.message, "warning");
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function uploadOntology(arrayBuffer) {
    var array = new Uint8Array(arrayBuffer);

    var stringByteArray = btoa(new Uint8Array(array).reduce(function (data, byte) {
        return data + String.fromCharCode(byte);
    }, ''));

    $.ajax({
        url: "Upload/UploadOntologyFile",
        dataType: "json",
        data: {
            "stringByteArray": stringByteArray
        },
        type: "POST",
        success: function (uploadOntologyResponse) {
            if (uploadOntologyResponse !== null) {
                if (uploadOntologyResponse.ontology !== undefined && uploadOntologyResponse.ontology !== null) {
                    $("#treeview-ontology").append(uploadOntologyResponse.ontology.HtmlTreeViewMenu);

                    createContextMenusForTreeView();

                    var message = "Ontology " + uploadOntologyResponse.ontology.OntologyName + " loaded into Grasews.";

                    console.log(message);

                    addToastMessage(message, "success");

                    addToastMessage("Ontology saved in the user's repository.", "success");
                }
                else {
                    addToastMessage(uploadOntologyResponse.message, "warning");
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function modalOpenOntology() {
    $.ajax({
        url: "Site/_ModalOpenOntology",
        type: "GET",
        cache: false
    }).done(function (result) {
        $("#place-holder-partial-views").html(result);
        $('#modal-open-ontology').modal('toggle');
        load_select2ToHtml();

        $('#modal-open-ontology').on('shown.bs.modal', function () {
            $("#modal-open-ontology").find("#fileOpenOwl").trigger('focus');
        });

        $("#nav-tab-open-new-ontology").on('shown.bs.tab', function (e) {
            $("#modal-open-ontology").find("#fileOpenOwl").trigger('focus');
        });

        $("#nav-tab-open-saved-ontology").on('shown.bs.tab', function (e) {
            $("#modal-open-ontology").find(".select2").trigger('focus');
        });

        $("#lnkOpenSavedOntology").on("click", function () {
            var idOntology = $("#ddl-saved-ontologies").val();
            openSavedOntology(idOntology);
        });

        $("#lnkUploadOntology").on("click", function () {
            var file = document.getElementById("fileOpenOwl").files[0];
            //var filename = file.name.replace(/\.[^/.]+$/, "");
            var reader = new FileReader();

            reader.onload = function () {
                uploadOntology(this.result);
                $("#fileOpenOwl").val("");
            };

            reader.readAsArrayBuffer(file);
        });
    });
}

function getOntologiesAlreadyOpened() {
    $.ajax({
        url: "Site/GetOntologiesAlreadyOpened",
        type: "GET",
        success: function (ontologies) {
            if (ontologies !== null && ontologies.length > 0) {
                $.each(ontologies, function (index, ontology) {
                    $("#treeview-ontology").append(ontology.HtmlTreeViewMenu);

                    createContextMenusForTreeView();

                    addOntologyToDropDownOnAddModelReferenceModal(ontology);

                    var message = "Ontology '" + ontology.OntologyName + "' loaded into Grasews from previous session.";

                    console.log(message);

                    addToastMessage(message, "success");
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

/* ---------------------- Ontologies : END ---------------------- */

/* ---------------------- Sawsdl Model Reference : BEGIN ---------------------- */

function modalAddModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalAddModelReference",
        type: "GET",
        cache: false
    }).done(function (result) {
        if (result !== null && result !== "") {
            $("#place-holder-partial-views").html(result);
            $('#modal-add-model-reference').modal('toggle');
            load_select2ToHtml();

            var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
            $("#lblElementToAddModelReference").text(textToShow);

            $("#lnkApplyModelReference").unbind("click");

            $("#lnkApplyModelReference").on("click", function () {
                addModelReference(idWsdlElement, idWsdlElementType, idServiceDescription);
            });

            $("#ddl-add-modal-reference-ontologies").on("change", function () {
                var idOntology = $(this).val();

                var ddl = $("#ddl-add-modal-reference-ontology-terms");
                ddl.prop("disabled", true);
                ddl.empty();

                if (idOntology !== "none") {
                    loadTermsInDropdownList(idOntology);
                }
            });
        }
        else {
            addToastMessage("No ontologies loaded into Grasews.", "warning");
        }
    });
}

function loadTermsInDropdownList(idOntology) {
    $.ajax({
        url: "Site/_ModalAddModelReferenceOntologyTerms",
        type: "GET",
        data: {
            "idOntology": idOntology
        },
        cache: false
    }).done(function (result) {
        var ddl = $("#ddl-add-modal-reference-ontology-terms");
        ddl.append(result);
        ddl.prop("disabled", false);
        ddl.on("change", function () {
            var idOntologyTerm = $(this).val();

            if (idOntologyTerm !== "none") {
                $("#lnkApplyModelReference").prop("disabled", false);
            }
            else {
                $("#lnkApplyModelReference").prop("disabled", true);
            }
        });
    });
}

function addModelReference(idWsdlElement, idWsdlElementType, idServiceDescription) {
    var idOntologyTerm = $("#ddl-add-modal-reference-ontology-terms option:selected").val();

    $.ajax({
        url: "Site/AddSawsdlModelReference",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription,
            "idOntologyTerm": idOntologyTerm
        },
        type: 'POST',
        success: function (addModelReferenceResponse) {
            if (addModelReferenceResponse) {
                fillCytoscape(JSON.parse(addModelReferenceResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(addModelReferenceResponse.serviceDescription.GraphJson));

                fillCodeMirror(addModelReferenceResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(addModelReferenceResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(addModelReferenceResponse.serviceDescription.Id);

                var message = "SAWSDL Model Reference added successfuly to the service description '" + addModelReferenceResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function modalRemoveModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalRemoveModelReference",
        type: "GET",
        data: {
            "idServiceDescription": idServiceDescription,
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType
        },
        success: function (result) {
            if (result !== null && result !== "") {
                $("#place-holder-partial-views").html(result);
                var $modal = $('#modal-remove-model-reference');
                $modal.modal('toggle');

                var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
                $("#lblElementToRemoveModelReference").text(textToShow);

                addClickFunctionToEachModelReferenceToBeRemoved();

                $("#lnkApplyRemoveModelReference").on("click", function () {
                    var messageToConfirmModelReferenceRemoval = "Are you sure you want to remove the annotation(s) below?\n";

                    var listOfModelReferencesRemoved = $modal.find("#model-references-urls-to-remove").val().split(",");

                    for (var i = 0; i < listOfModelReferencesRemoved.length; i++) {
                        messageToConfirmModelReferenceRemoval += "\n" + listOfModelReferencesRemoved[i];
                    }

                    if (confirm(messageToConfirmModelReferenceRemoval)) {
                        var idServiceDescription = $modal.find("#id-service-description").val();
                        var idWsdlElement = $modal.find("#id-wsdl-element").val();
                        var idWsdlElementType = $modal.find("#id-wsdl-element-type").val();
                        var idOntologyTermsToRemove = $modal.find("#id-ontology-terms-to-remove").val();

                        removeModelReference(idServiceDescription, idWsdlElement, idWsdlElementType, idOntologyTermsToRemove);
                    }
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function removeModelReference(idServiceDescription, idWsdlElement, idWsdlElementType, idOntologyTermsToRemove) {
    $.ajax({
        url: "Site/RemoveSawsdlModelReference",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription,
            "idOntologyTermsToRemove": idOntologyTermsToRemove
        },
        type: 'POST',
        success: function (removeModelReferenceResponse) {
            if (removeModelReferenceResponse) {
                fillCytoscape(JSON.parse(removeModelReferenceResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(removeModelReferenceResponse.serviceDescription.GraphJson));

                fillCodeMirror(removeModelReferenceResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(removeModelReferenceResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(removeModelReferenceResponse.serviceDescription.Id);

                var message = "SAWSDL Model Reference(s) removed successfuly from the service description '" + removeModelReferenceResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function addClickFunctionToEachModelReferenceToBeRemoved() {
    var listModelReferencesRemoved = [];
    var listIdOntologyTermsRemoved = [];

    $("#divListModelReferenceToRemove > a").each(function (index) {
        $(this).on("click", function () {
            listModelReferencesRemoved.push($(this).find("span")[0].innerText);
            listIdOntologyTermsRemoved.push($(this).attr("id-ontology-term"));

            var $modal = $("#modal-remove-model-reference");

            $modal.find("#model-references-urls-to-remove").val(listModelReferencesRemoved.join());
            $modal.find("#id-ontology-terms-to-remove").val(listIdOntologyTermsRemoved.join());

            this.remove();
        });
    });
}

function modalAddModelReferenceFromOntologyTerm(idOntologyTerm) {
    $.ajax({
        url: "Site/_ModalAddModelReferenceFromOntologyTerm",
        type: "GET",
        cache: false
    }).done(function (result) {
        $("#place-holder-partial-views").html(result);
        $('#modal-add-model-reference-from-ontology').modal('toggle');
        load_select2ToHtml();

        $("#lnkApplyModelReferenceFromOntology").unbind("click");

        $("#lnkApplyModelReferenceFromOntology").on("click", function () {
            addModelReferenceFromOntology(idOntologyTerm);
        });
    });
}

function addModelReferenceFromOntology(idOntologyTerm) {
    var $ddl_add_modal_reference_wsdl_element = $("#ddl-add-modal-reference-wsdl-element option:selected");

    var idWsdlElement = $ddl_add_modal_reference_wsdl_element.val();
    var idWsdlElementType = $ddl_add_modal_reference_wsdl_element.attr("id-wsdl-element-type");
    var idServiceDescription = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    $.ajax({
        url: "Site/AddSawsdlModelReference",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription,
            "idOntologyTerm": idOntologyTerm
        },
        type: 'POST',
        success: function (addModelReferenceResponse) {
            if (addModelReferenceResponse) {
                fillCytoscape(JSON.parse(addModelReferenceResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(addModelReferenceResponse.serviceDescription.GraphJson));

                fillCodeMirror(addModelReferenceResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(addModelReferenceResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(addModelReferenceResponse.serviceDescription.Id);

                var message = "SAWSDL Model Reference added successfuly to the service description '" + addModelReferenceResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

/* ---------------------- Sawsdl Model Reference : END ---------------------- */

/* ---------------------- Sawsdl Lifting and Lowering Schema Mapping : BEGIN ---------------------- */

function modalAddLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalAddLiftingSchemaMapping",
        type: "GET",
        cache: false
    }).done(function (result) {
        $("#place-holder-partial-views").html(result);
        $('#modal-lifting-schema-mapping').modal('toggle');

        var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
        $("#lblElementToAddLiftingSchemaMapping").text(textToShow);

        $("#lnk-apply-lifting-schema-mapping").on("click", function () {
            addLiftingSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription);
        });
    });
}

function modalAddLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalAddLoweringSchemaMapping",
        type: "GET",
        cache: false
    }).done(function (result) {
        $("#place-holder-partial-views").html(result);
        $('#modal-lowering-schema-mapping').modal('toggle');

        var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
        $("#lblElementToAddLoweringSchemaMapping").text(textToShow);

        $("#lnk-apply-lowering-schema-mapping").on("click", function () {
            addLoweringSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription);
        });
    });
}

function modalRemoveLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalRemoveLoweringSchemaMapping",
        type: "GET",
        data: {
            "idServiceDescription": idServiceDescription,
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType
        },
        success: function (result) {
            if (result !== null && result !== "") {
                $("#place-holder-partial-views").html(result);
                var $modal = $('#modal-remove-lowering-schema-mapping');
                $modal.modal('toggle');

                var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
                $("#lblElementToRemoveLoweringSchemaMapping").text(textToShow);

                $("#lnkApplyRemoveLoweringSchemaMapping").on("click", function () {
                    var messageToConfirmLoweringSchemaMappingRemoval = "Are you sure you want to remove the lowering schema mapping?";

                    if (confirm(messageToConfirmLoweringSchemaMappingRemoval)) {
                        var idServiceDescription = $modal.find("#id-service-description").val();
                        var idWsdlElement = $modal.find("#id-wsdl-element").val();
                        var idWsdlElementType = $modal.find("#id-wsdl-element-type").val();

                        removeLoweringSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription);
                    }
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function modalRemoveLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription) {
    $.ajax({
        url: "Site/_ModalRemoveLiftingSchemaMapping",
        type: "GET",
        data: {
            "idServiceDescription": idServiceDescription,
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType
        },
        success: function (result) {
            if (result !== null && result !== "") {
                $("#place-holder-partial-views").html(result);
                var $modal = $('#modal-remove-lifting-schema-mapping');
                $modal.modal('toggle');

                var textToShow = "[" + wsdlElementType + "] " + wsdlElementName;
                $("#lblElementToRemoveLiftingSchemaMapping").text(textToShow);

                $("#lnkApplyRemoveLiftingSchemaMapping").on("click", function () {
                    var messageToConfirmLiftingSchemaMappingRemoval = "Are you sure you want to remove the lifting schema mapping?";

                    if (confirm(messageToConfirmLiftingSchemaMappingRemoval)) {
                        var idServiceDescription = $modal.find("#id-service-description").val();
                        var idWsdlElement = $modal.find("#id-wsdl-element").val();
                        var idWsdlElementType = $modal.find("#id-wsdl-element-type").val();

                        removeLiftingSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription);
                    }
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function addLiftingSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription) {
    var liftingSchemaMappingUrl = $("#txtLiftingSchemaMappingUrl").val();

    $.ajax({
        url: "Site/AddSawsdlLiftingSchemaMapping",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription,
            "liftingSchemaMappingUrl": liftingSchemaMappingUrl
        },
        type: 'POST',
        success: function (addLiftingSchemaMappingResponse) {
            if (addLiftingSchemaMappingResponse.ServiceDescription !== undefined && addLiftingSchemaMappingResponse.ServiceDescription !== null) {
                fillCytoscape(JSON.parse(addLiftingSchemaMappingResponse.ServiceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(addLiftingSchemaMappingResponse.ServiceDescription.GraphJson));

                fillCodeMirror(addLiftingSchemaMappingResponse.ServiceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(addLiftingSchemaMappingResponse.ServiceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(addLiftingSchemaMappingResponse.ServiceDescription.Id);

                var message = "SAWSDL Lifting Schema Mapping added successfuly to the service description '" + addLiftingSchemaMappingResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage(addLiftingSchemaMappingResponse.Exception.Message, "warning");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function addLoweringSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription) {
    var loweringSchemaMappingUrl = $("#txtLoweringSchemaMappingUrl").val();

    $.ajax({
        url: "Site/AddSawsdlLoweringSchemaMapping",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription,
            "loweringSchemaMappingUrl": loweringSchemaMappingUrl
        },
        type: 'POST',
        success: function (addLoweringSchemaMappingResponse) {
            if (addLoweringSchemaMappingResponse) {
                fillCytoscape(JSON.parse(addLoweringSchemaMappingResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(addLoweringSchemaMappingResponse.serviceDescription.GraphJson));

                fillCodeMirror(addLoweringSchemaMappingResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(addLoweringSchemaMappingResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(addLoweringSchemaMappingResponse.serviceDescription.Id);

                var message = "SAWSDL Lowering Schema Mapping added successfuly to the service description '" + addLoweringSchemaMappingResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function removeLiftingSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription) {
    $.ajax({
        url: "Site/RemoveLiftingSchemaMapping",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription
        },
        type: 'POST',
        success: function (removeLiftingSchemaMappingResponse) {
            if (removeLiftingSchemaMappingResponse) {
                fillCytoscape(JSON.parse(removeLiftingSchemaMappingResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(removeLiftingSchemaMappingResponse.serviceDescription.GraphJson));

                fillCodeMirror(removeLiftingSchemaMappingResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(removeLiftingSchemaMappingResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(removeLiftingSchemaMappingResponse.serviceDescription.Id);

                var message = "SAWSDL Lifting Schema Mapping removed successfuly from the service description '" + removeLiftingSchemaMappingResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

function removeLoweringSchemaMapping(idWsdlElement, idWsdlElementType, idServiceDescription) {
    $.ajax({
        url: "Site/RemoveLoweringSchemaMapping",
        data: {
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType,
            "idServiceDescription": idServiceDescription
        },
        type: 'POST',
        success: function (removeLoweringSchemaMappingResponse) {
            if (removeLoweringSchemaMappingResponse) {
                fillCytoscape(JSON.parse(removeLoweringSchemaMappingResponse.serviceDescription.GraphJson));

                adjustUserNodesPositionsOnGraph(JSON.parse(removeLoweringSchemaMappingResponse.serviceDescription.GraphJson));

                fillCodeMirror(removeLoweringSchemaMappingResponse.serviceDescription.Xml);

                $("#li-service-description-" + idServiceDescription).empty();
                $("#li-service-description-" + idServiceDescription).html($(removeLoweringSchemaMappingResponse.serviceDescription.HtmlTreeViewMenu)[0].innerHTML);

                createContextMenusForTreeView();

                hideAllServiceDescriptionsButThis(removeLoweringSchemaMappingResponse.serviceDescription.Id);

                var message = "SAWSDL Lowering Schema Mapping removed successfuly from the service description '" + removeLoweringSchemaMappingResponse.serviceDescription.ServiceName + "'.";

                console.log(message);

                addToastMessage(message, "success");

                window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
            }
            else {
                addToastMessage("Error", "danger");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
        }
    });
}

/* ---------------------- Sawsdl Lifting and Lowering Schema Mapping : END ---------------------- */

/* ---------------------- Share Service Description : BEGIN ---------------------- */

function modalShareServiceDescription() {
    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        $.ajax({
            url: "Site/_ModalShareServiceDescription",
            type: "GET",
            success: function (result) {
                $("#place-holder-partial-views").html(result);
                $("#modal-share").modal("toggle");

                load_select2ToHtml();

                $('#modal-share').on('shown.bs.modal', function () {
                    $("#modal-share").find(".select2 input[type=search]").trigger('focus');
                });

                $("#lnk-share").click(function () {
                    shareServiceDescription();
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
            }
        });
    }
    else {
        addToastMessage("There is no service description opened to be shared.", "warning");
    }
}

function shareServiceDescription() {
    var selectedUsers = $("#ddl-select-users-to-share").select2("data");
    var count = selectedUsers.length;

    var usersToShare = [];

    $.each(selectedUsers, function (index, user) {
        usersToShare.push(user.id);

        if (!--count) {
            var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);
            var idOntologiesOpened = getCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);
            var data;

            if (idOntologiesOpened) {
                data = {
                    "emails": usersToShare,
                    "idServiceDescription": idServiceDescriptionOnView,
                    "idOntologies": idOntologiesOpened
                };
            } else {
                data = {
                    "emails": usersToShare,
                    "idServiceDescription": idServiceDescriptionOnView
                };
            }

            $.ajax({
                url: "Site/ShareServiceDescription",
                data: data,
                type: 'POST',
                success: function (result) {
                    if (result !== null) {
                        if (result.shareResponse !== undefined && result.shareResponse !== "") {
                            $("#modal-share").modal("toggle");
                        }
                        else {
                            addToastMessage(result.message, "warning");
                        }
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
                }
            });
        }
    });
}

/* ---------------------- Share Service Description : END ---------------------- */

/* ---------------------- Task : BEGIN ---------------------- */

function modalAddTask() {
    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        $.ajax({
            url: "Site/_ModalAddTask",
            type: "GET",
            cache: false
        }).done(function (result) {
            $("#place-holder-partial-views").html(result);
            $('#modal-task').modal('toggle');

            $("#lnk-add-task").on("click", function () {
                addTask();
            });
        });
    }
    else {
        addToastMessage("There is no service description opened to create a task.", "warning");
    }
}

function modalTaskList() {
    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        $.ajax({
            url: "Site/_ModalTaskList",
            type: "GET",
            data: {
                "idServiceDescription": idServiceDescriptionOnView
            },
            cache: false
        }).done(function (result) {
            $("#place-holder-partial-views").html(result);
            $('#modal-task-list').modal('toggle');
            init_ButtonCheckbox();

            $(".btn-task-done").on("click", function () {
                var $this = $(this);
                var idTask = $this.attr("id-task");
                var $boxPanel = $this.closest("div.panel.box");
                var isDone = $this.hasClass("active");
                var taskHasComments = $this.attr("task-has-comments");

                markTaskAsDone(idTask, $boxPanel, isDone, taskHasComments);
            });

            $(".btn-create-task-comment").on("click", function () {
                var $this = $(this);
                var idTask = $this.attr("id-task");
                var taskComment = $("#txt-new-comment-" + idTask).val();
                if (taskComment !== "") {
                    addTaskComment(idTask, taskComment);
                    $("#txt-new-comment-" + idTask).val("");
                }
                $("#txt-new-comment-" + idTask).focus();
            });
        });
    }
    else {
        addToastMessage("There is no service description opened to view the task list.", "warning");
    }
}

function markTaskAsDone(idTask, $boxPanel, isDone, taskHasComments) {
    $.ajax({
        url: "Site/MarkTaskAsDone",
        data: {
            "idTask": idTask,
            "done": isDone
        },
        type: "POST",
        cache: false
    }).done(function (result) {
        changeTaskStatus(idTask, $boxPanel, isDone, taskHasComments);
    });
}

function changeTaskStatus(idTask, $boxPanel, isDone, taskHasComments) {
    var $divTaskStatusIcon = $("#div-task-status-icon-" + idTask);

    if (isDone) {
        $boxPanel.removeClass("box-primary");
        $boxPanel.removeClass("box-danger");
        $boxPanel.addClass("box-success");

        $divTaskStatusIcon.find("i.task-done").removeClass("hidden");
        $divTaskStatusIcon.find("i.task-created").addClass("hidden");
        $divTaskStatusIcon.find("i.task-with-comments").addClass("hidden");

        disableNewComment(true, idTask);
    }
    else {
        if (taskHasComments) {
            $boxPanel.addClass("box-primary");
            $boxPanel.removeClass("box-danger");
            $boxPanel.removeClass("box-success");

            $divTaskStatusIcon.find("i.task-done").addClass("hidden");
            $divTaskStatusIcon.find("i.task-created").addClass("hidden");
            $divTaskStatusIcon.find("i.task-with-comments").removeClass("hidden");
        }
        else {
            $boxPanel.removeClass("box-primary");
            $boxPanel.addClass("box-danger");
            $boxPanel.removeClass("box-success");

            $divTaskStatusIcon.find("i.task-done").addClass("hidden");
            $divTaskStatusIcon.find("i.task-created").removeClass("hidden");
            $divTaskStatusIcon.find("i.task-with-comments").addClass("hidden");
        }

        disableNewComment(false, idTask);
    }
}

function disableNewComment(value, idTask) {
    var $txtNewComment = $("#txt-new-comment-" + idTask);
    var $btnCreateTaskComment = $("#btn-create-task-comment-" + idTask);

    if (value) {
        $txtNewComment.attr("readonly", "readonly");
        $txtNewComment.attr("disabled", "disabled");

        $btnCreateTaskComment.removeClass("animated-hover");
        $btnCreateTaskComment.attr("readonly", "readonly");
        $btnCreateTaskComment.attr("disabled", "disabled");
    }
    else {
        $txtNewComment.removeAttr("readonly");
        $txtNewComment.removeAttr("disabled");

        $btnCreateTaskComment.addClass("animated-hover");
        $btnCreateTaskComment.removeAttr("readonly");
        $btnCreateTaskComment.removeAttr("disabled");
    }
}

function addTask() {
    var taskTitle = $("#txt-task-title").val();
    var taskDescription = $("#txt-task-description").val();

    var idServiceDescription = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    $.ajax({
        url: "Site/AddTask",
        type: "POST",
        data: {
            "idServiceDescription": idServiceDescription,
            "taskTitle": taskTitle,
            "taskDescription": taskDescription
        },
        cache: false
    }).done(function (result) {
        window.dispatchEvent(notifyTaskCreatedEvent);

        console.log(result.Message);
        addToastMessage(result.Message, "success");
    });
}

function addTaskComment(idTask, taskComment) {
    $.ajax({
        url: "Site/AddTaskComment",
        type: "POST",
        data: {
            "idTask": idTask,
            "comment": taskComment
        },
        cache: false
    }).done(function (result) {
        if (result !== null) {
            var newRow = "<tr><td>" + result.UserEmail + "</td><td>" + result.TaskComment + "</td></tr>";
            var $lastRow = $("#div-task-comments-" + idTask + " table tbody tr:last");
            var $divPreviousComments = $("#div-task-previous-comments-" + idTask);
            var isDivHidden = $divPreviousComments.hasClass("hidden");

            if (isDivHidden) {
                $divPreviousComments.removeClass("hidden");
            }

            if ($lastRow.length) {
                $lastRow.after(newRow);
            }
            else {
                $("#div-task-comments-" + idTask + " table tbody").append(newRow);
            }
            $("#div-task-comments-" + idTask).animate({ scrollTop: $("#div-task-comments-" + idTask).prop("scrollHeight") }, 1500);

            var taskHasComments = true;
            var isDone = false;
            var $boxPanel = $("#panel-box-" + idTask);

            changeTaskStatus(idTask, $boxPanel, isDone, taskHasComments);

            window.dispatchEvent(notifyTaskCommentCreatedEvent);

            console.log(result.Message);
            addToastMessage(result.Message, "success");
        }
    });
}

/* ---------------------- Task : END ---------------------- */

/* ---------------------- Issue : BEGIN ---------------------- */

function modalAddIssue() {
    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        $.ajax({
            url: "Site/_ModalAddIssue",
            type: "GET",
            cache: false
        }).done(function (result) {
            $("#place-holder-partial-views").html(result);
            $('#modal-issue').modal('toggle');
            load_select2ToHtml();

            $("#lnk-add-issue").on("click", function () {
                addIssue();
            });
        });
    }
    else {
        addToastMessage("There is no service description opened to create an issue.", "warning");
    }
}

function modalIssueList() {
    var idServiceDescriptionOnView = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    if (idServiceDescriptionOnView !== null && idServiceDescriptionOnView !== "") {
        $.ajax({
            url: "Site/_ModalIssueList",
            type: "GET",
            data: {
                "idServiceDescription": idServiceDescriptionOnView
            },
            cache: false
        }).done(function (result) {
            $("#place-holder-partial-views").html(result);
            $('#modal-issue-list').modal('toggle');
            init_ButtonCheckbox();

            $(".btn-issue-done").on("click", function () {
                var $this = $(this);
                var idIssue = $this.attr("id-issue");
                var $boxPanel = $this.closest("div.panel.box");
                var isSolved = $this.hasClass("active");
                var issueHasAnswers = $this.attr("issue-has-answers");

                markIssueAsSolved(idIssue, $boxPanel, isSolved, issueHasAnswers);
            });

            $(".btn-create-issue-answer").on("click", function () {
                var $this = $(this);
                var idIssue = $this.attr("id-issue");
                var issueAnswer = $("#txt-new-answer-" + idIssue).val();
                if (issueAnswer !== "") {
                    addIssueAnswer(idIssue, issueAnswer);
                    $("#txt-new-answer-" + idIssue).val("");
                }
                $("#txt-new-answer-" + idIssue).focus();
            });
        });
    }
    else {
        addToastMessage("There is no service description opened to view the issue list.", "warning");
    }
}

function markIssueAsSolved(idIssue, $boxPanel, isSolved, issueHasAnswers) {
    var idWsdlElement = $("#hidden-issue-" + idIssue + "-id-service-description-element").val();
    var idWsdlElementType = $("#hidden-issue-" + idIssue + "-id-service-description-element-type").val();

    changeIssueStatus(idIssue, $boxPanel, isSolved, issueHasAnswers);

    showAjaxLoading = false;

    $.ajax({
        url: "Site/MarkIssueAsSolved",
        data: {
            "idIssue": idIssue,
            "solved": isSolved,
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType
        },
        type: "POST",
        cache: false,
        success: function (updateIssueResponse) {
            fillCytoscape(JSON.parse(updateIssueResponse.Issue.ServiceDescription.GraphJson));

            adjustUserNodesPositionsOnGraph(JSON.parse(updateIssueResponse.Issue.ServiceDescription.GraphJson));

            //changeIssueStatus(idIssue, $boxPanel, isSolved, issueHasAnswers);

            window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
        },
        complete: function () {
            showAjaxLoading = true;
        }
    });
}

function changeIssueStatus(idIssue, $boxPanel, isSolved, issueHasAnswers) {
    var $divIssueStatusIcon = $("#div-issue-status-icon-" + idIssue);

    if (isSolved) {
        $boxPanel.removeClass("box-primary");
        $boxPanel.removeClass("box-danger");
        $boxPanel.addClass("box-success");

        $divIssueStatusIcon.find("i.issue-solved").removeClass("hidden");
        $divIssueStatusIcon.find("i.issue-created").addClass("hidden");
        $divIssueStatusIcon.find("i.issue-with-answers").addClass("hidden");

        disableNewAnswer(true, idIssue);
    }
    else {
        if (issueHasAnswers) {
            $boxPanel.addClass("box-primary");
            $boxPanel.removeClass("box-danger");
            $boxPanel.removeClass("box-success");

            $divIssueStatusIcon.find("i.issue-solved").addClass("hidden");
            $divIssueStatusIcon.find("i.issue-created").addClass("hidden");
            $divIssueStatusIcon.find("i.issue-with-answers").removeClass("hidden");
        }
        else {
            $boxPanel.removeClass("box-primary");
            $boxPanel.addClass("box-danger");
            $boxPanel.removeClass("box-success");

            $divIssueStatusIcon.find("i.issue-solved").addClass("hidden");
            $divIssueStatusIcon.find("i.issue-created").removeClass("hidden");
            $divIssueStatusIcon.find("i.issue-with-answers").addClass("hidden");
        }

        disableNewAnswer(false, idIssue);
    }
}

function disableNewAnswer(value, idIssue) {
    var $txtNewAnswer = $("#txt-new-answer-" + idIssue);
    var $btnCreateIssueAnswer = $("#btn-create-issue-answer-" + idIssue);

    if (value) {
        $txtNewAnswer.attr("readonly", "readonly");
        $txtNewAnswer.attr("disabled", "disabled");

        $btnCreateIssueAnswer.removeClass("animated-hover");
        $btnCreateIssueAnswer.attr("readonly", "readonly");
        $btnCreateIssueAnswer.attr("disabled", "disabled");
    }
    else {
        $txtNewAnswer.removeAttr("readonly");
        $txtNewAnswer.removeAttr("disabled");

        $btnCreateIssueAnswer.addClass("animated-hover");
        $btnCreateIssueAnswer.removeAttr("readonly");
        $btnCreateIssueAnswer.removeAttr("disabled");
    }
}

function addIssue() {
    var issueTitle = $("#txt-issue-title").val();
    var issueDescription = $("#txt-issue-description").val();

    var $ddl_add_issue_wsdl_element = $("#ddl-add-issue-wsdl-element option:selected");

    var idWsdlElement = $ddl_add_issue_wsdl_element.val();
    var idWsdlElementType = $ddl_add_issue_wsdl_element.attr("id-wsdl-element-type");

    var idServiceDescription = getCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

    $.ajax({
        url: "Site/AddIssue",
        type: "POST",
        data: {
            "idServiceDescription": idServiceDescription,
            "issueTitle": issueTitle,
            "issueDescription": issueDescription,
            "idWsdlElement": idWsdlElement,
            "idWsdlElementType": idWsdlElementType
        },
        cache: false,
        success: function (addIssueResponse) {
            fillCytoscape(JSON.parse(addIssueResponse.Issue.ServiceDescription.GraphJson));

            adjustUserNodesPositionsOnGraph(JSON.parse(addIssueResponse.Issue.ServiceDescription.GraphJson));

            window.dispatchEvent(notifyIssueCreatedEvent);

            console.log(addIssueResponse.Message);
            addToastMessage(addIssueResponse.Message, "success");

            window.dispatchEvent(notifyServiceDescriptionUpdateEvent);
        }
    });
}

function addIssueAnswer(idIssue, issueAnswer) {
    $.ajax({
        url: "Site/AddIssueAnswer",
        type: "POST",
        data: {
            "idIssue": idIssue,
            "answer": issueAnswer
        },
        cache: false
    }).done(function (result) {
        if (result !== null) {
            var newRow = "<tr><td>" + result.UserEmail + "</td><td>" + result.IssueAnswer + "</td></tr>";
            var $lastRow = $("#div-task-comments-" + idIssue + " table tbody tr:last");
            var $divPreviousAnswers = $("#div-issue-previous-answers-" + idIssue);
            var isDivHidden = $divPreviousAnswers.hasClass("hidden");

            if (isDivHidden) {
                $divPreviousAnswers.removeClass("hidden");
            }

            if ($lastRow.length) {
                $lastRow.after(newRow);
            }
            else {
                $("#div-issue-answers-" + idIssue + " table tbody").append(newRow);
            }
            $("#div-issue-answers-" + idIssue).animate({ scrollTop: $("#div-issue-answers-" + idIssue).prop("scrollHeight") }, 1500);

            var issueHasAnswers = true;
            var isSolved = false;
            var $boxPanel = $("#panel-box-" + idIssue);

            changeIssueStatus(idIssue, $boxPanel, isSolved, issueHasAnswers);

            window.dispatchEvent(notifyIssueAnswerCreatedEvent);

            console.log(result.Message);
            addToastMessage(result.Message, "success");
        }
    });
}

/* ---------------------- Issue : END ---------------------- */

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function reOpenWebServiceAfterOperation(createElementsWebServiceOperationResponse) {
    fillCytoscape(createElementsWebServiceOperationResponse.cytoscape);
    fillWebServiceTreeView(createElementsWebServiceOperationResponse.wsdlDocument);
    fillCodeMirror(createElementsWebServiceOperationResponse.wsdlDocument.WsdlRaw);
    saveWebServiceInSession(createElementsWebServiceOperationResponse.wsdlDocument);

    $("#div-service-description-in-view").show();
    $("#lbl-service-description-name-in-view").text(createElementsWebServiceOperationResponse.wsdlDocument.ServiceName);

    updateServiceDescription();
}

function addOntologyToDropDownOnAddModelReferenceModal(ontology) {
    var $ddl = $("#ddl-add-modal-reference-ontology");
    var html = '<option id="opt-' + ontology.Id + '" value="' + ontology.Id + '">' + ontology.OntologyName + '</option>';
    $ddl.append(html);
}

function removeOntologyFromDropDownOnAddModelReferenceModal(idOntology) {
    var $ddl = $("#ddl-add-modal-reference-ontology");
    var $opt = $ddl.find("#opt-" + idOntology);
    $opt.remove();
}

function createListOfTerms(data, color) {
    var html = "";

    var idOntology = data.Id;

    var termDisplayName;

    $.each(data.Terms, function (termIndex, term) {
        termDisplayName = term.TermName;

        if (term.TermName.length > 21) {
            termDisplayName = term.TermName.substring(0, 20) + '...';
        }

        html = html + '<li class="treeview ontologyElementContextMenu" ontology-id="' + idOntology + '" term-id="' + term.Id + '">';
        html = html + ' <a href="#">';
        html = html + '     <i class="fa fa-circle-o" style="color: ' + color + '"></i>';
        html = html + '     <span data-toggle="tooltip" data-placement="top" title="' + term.TermName + '">';
        html = html + termDisplayName;
        html = html + '     </span>';

        var hasChildrenTerms = term.Terms !== null && term.Terms.length > 0 && term.Terms[0] !== null;

        if (hasChildrenTerms) {
            html = html + ' <span class="pull-right-container">';
            html = html + '     <i class="fa fa-angle-right pull-left"></i>';
            html = html + ' </span>';
        }

        html = html + ' </a>';

        if (hasChildrenTerms) {
            html = html + '<ul class="treeview-menu">';
            html = html + createListOfTerms(term, color);
            html = html + '</ul>';
        }

        html = html + '</li>';
    });

    return html;
}