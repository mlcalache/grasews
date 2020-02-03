/* Upgrade to https://swisnl.github.io/jQuery-contextMenu/ */

(function ($, window) {
    $.fn.contextMenu = function (settings) {
        return this.each(function () {
            // Open context menu
            $(this).on("contextmenu", function (e) {
                // return native menu if pressing control
                if (e.ctrlKey) return;

                var canModelReference = $(this).attr("can-model-reference");
                var canSchemaMapping = $(this).attr("can-schema-mapping");
                var hasModelReference = $(this).attr("has-model-reference");
                var hasLiftingSchemaMapping = $(this).attr("has-lifting-schema-mapping");
                var hasLoweringSchemaMapping = $(this).attr("has-lowering-schema-mapping");

                var $liAddModelReferenceContextMenu = $("#webserviceElementContextMenu .li-add-model-reference-context-menu");
                var $liRemoveModelReferenceContextMenu = $("#webserviceElementContextMenu .li-remove-model-reference-context-menu");

                var $liAddLiftingSchemaMappingContextMenu = $("#webserviceElementContextMenu .li-add-lifting-schema-mapping-context-menu");
                var $liRemoveLiftingSchemaMappingContextMenu = $("#webserviceElementContextMenu .li-remove-lifting-schema-mapping-context-menu");

                var $liAddLoweringSchemaMappingContextMenu = $("#webserviceElementContextMenu .li-add-lowering-schema-mapping-context-menu");
                var $liRemoveLoweringSchemaMappingContextMenu = $("#webserviceElementContextMenu .li-remove-lowering-schema-mapping-context-menu");

                if (canModelReference === "false") {
                    $liAddModelReferenceContextMenu.hide();
                }
                else {
                    $liAddModelReferenceContextMenu.show();

                    if (hasModelReference === "true") {
                        $liRemoveModelReferenceContextMenu.show();
                    }
                    else {
                        $liRemoveModelReferenceContextMenu.hide();
                    }
                }

                if (canSchemaMapping === "false") {
                    $("#webserviceElementContextMenu .divider").hide();
                    $liAddLiftingSchemaMappingContextMenu.hide();
                    $liAddLoweringSchemaMappingContextMenu.hide();
                    $liRemoveLiftingSchemaMappingContextMenu.hide();
                    $liRemoveLoweringSchemaMappingContextMenu.hide();
                }
                else {
                    $("#webserviceElementContextMenu .divider").show();

                    if (hasLiftingSchemaMapping === "true") {
                        $liAddLiftingSchemaMappingContextMenu.hide();
                        $liRemoveLiftingSchemaMappingContextMenu.show();
                    }
                    else {
                        $liAddLiftingSchemaMappingContextMenu.show();
                        $liRemoveLiftingSchemaMappingContextMenu.hide();
                    }

                    if (hasLoweringSchemaMapping === "true") {
                        $liAddLoweringSchemaMappingContextMenu.hide();
                        $liRemoveLoweringSchemaMappingContextMenu.show();
                    }
                    else {
                        $liAddLoweringSchemaMappingContextMenu.show();
                        $liRemoveLoweringSchemaMappingContextMenu.hide();
                    }
                }

                //open menu
                var $menu = $(settings.menuSelector)
                    .data("invokedOn", $(e.target))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop')
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        $menu.hide();

                        var $invokedOn = $menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                return false;
            });

            //make sure menu closes on any click
            $('body').click(function () {
                $(settings.menuSelector).hide();
            });
        });

        function getMenuPosition(mouse, direction, scrollDir) {
            var win = $(window)[direction]();
            var scroll = $(window)[scrollDir]();
            var menu = $(settings.menuSelector)[direction]();
            var position = mouse + scroll;

            // opening menu would pass the side of the page
            if (mouse + menu > win && menu < mouse) {
                position -= menu;
            }

            return position;
        }
    };
})(jQuery, window);

function createContextMenusForTreeView() {
    //Context menu for web service elements
    $(".webserviceElementContextMenu").contextMenu({
        menuSelector: "#webserviceElementContextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            var idWsdlElement = $(invokedOn).closest("li").attr("id-wsdl-element");
            var wsdlElementName = $(invokedOn).closest("li").attr("wsdl-element-name");
            var wsdlElementType = $(invokedOn).closest("li").attr("wsdl-element-type");
            var idWsdlElementType = $(invokedOn).closest("li").attr("id-wsdl-element-type");
            var idServiceDescription = $(invokedOn).closest("li").attr("id-service-description");

            switch ($(selectedMenu).attr('id')) {
                case "webserviceElementContextMenuAddModelReference":
                    modalAddModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
                case "webserviceElementContextMenuRemoveModelReference":
                    modalRemoveModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
                case "webserviceElementContextMenuAddLiftingSchemaMapping":
                    modalAddLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
                case "webserviceElementContextMenuRemoveLiftingSchemaMapping":
                    modalRemoveLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
                case "webserviceElementContextMenuAddLoweringSchemaMapping":
                    modalAddLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
                case "webserviceElementContextMenuRemoveLoweringSchemaMapping":
                    modalRemoveLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
                    break;
            }
        }
    });

    //Context menu for ontology elements
    $(".ontologyElementContextMenu").contextMenu({
        menuSelector: "#ontologyElementContextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            var idOntologyTerm = $(invokedOn).closest("li").attr("term-id");

            switch ($(selectedMenu).attr('id')) {
                case "ontologyElementContextMenuAddModelReference":
                    modalAddModelReferenceFromOntologyTerm(idOntologyTerm);
                    break;
            }
        }
    });

    //Context menu for web services (treeview headers)
    $(".webserviceContextMenu").contextMenu({
        menuSelector: "#webserviceContextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            var closest_parent_li = $($(invokedOn["0"]).closest("li")[0]);
            var serviceName = closest_parent_li.attr("service-name");
            var idServiceDescription = closest_parent_li.attr("id-service-description");

            if (idServiceDescription !== null) {
                switch ($(selectedMenu).attr('id')) {
                    case "webserviceContextMenuViewWebService":
                        if (confirm("Are you sure you want to view the " + serviceName + " service description in the graph and in the code viewer?")) {
                            viewServiceDescription(idServiceDescription);
                        }
                        break;
                    case "webserviceContextMenuCloseWebService":
                        if (confirm("Are you sure you want to close the " + serviceName + " service description?")) {
                            closeServiceDescription(idServiceDescription);
                        }
                        break;
                }
            }
        }
    });

    //Context menu for ontologies (treeview headers)
    $(".ontologyContextMenu").contextMenu({
        menuSelector: "#ontologyContextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            var ontologyName = $(invokedOn["0"].parentElement.parentElement).attr("ontology-name");
            var ontologyId = $(invokedOn["0"].parentElement.parentElement).attr("ontology-id");

            if (ontologyId !== null) {
                switch ($(selectedMenu).attr('id')) {
                    case "ontologyContextMenuCloseOntology":
                        if (confirm("Are you sure you want to close the " + ontologyName + " ontology?")) {
                            closeOntology(ontologyId);
                        }
                }
            }
        }
    });
}

function closeOntology(ontologyId) {

    var ontologiesCookie = getCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);

    if (ontologiesCookie !== null) {
        var ontologiesIdsOpened = ontologiesCookie.split(",");
        var ontologiesIdsToKeepOpened = [];

        $.each(ontologiesIdsOpened, function (index, ontologyIdOpened) {
            if (ontologyIdOpened !== ontologyId) {
                ontologiesIdsToKeepOpened.push(ontologyIdOpened);
            }
        });

        var newOntologiesCookie = ontologiesIdsToKeepOpened.join();

        if (newOntologiesCookie !== "") {
            setCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE, newOntologiesCookie);
        }
        else {
            eraseCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);
        }

        $("#li-ontology-" + ontologyId).remove();
        removeOntologyFromDropDownOnAddModelReferenceModal(ontologyId);
    }

    //$.ajax({
    //    url: "Site/CloseOntology",
    //    data: {
    //        "ontologyName": ontologyId
    //    },
    //    success: function (result) {
    //        if (result.removed) {
    //            $("#li-ontology-" + result.ontologyName).remove();
    //            removeOntologyFromDropDownOnAddModelReferenceModal(result.ontologyId);

    //            if (callback != null) {
    //                if (callbackParameters != null)
    //                    callback(callbackParameters);
    //                else
    //                    callback();
    //            }
    //        }
    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
    //    }
    //});
}