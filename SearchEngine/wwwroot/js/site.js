$(document).ready(() => {

    const ExecuteDoSearch = (route, searchType) => {
        var editorName = `[data-core-${searchType}-searchtemplate-edit]`;
        const template = $(editorName).val();
        doSearch(route, template).done(html => {
            $("#searchResultContainer").html(html);
        });
    };
    const doSearch = (route, template) => {
        var deferred = $.Deferred();
        $.ajax({
            url: route,
            type: "post",
            dataType: "html",
            data: {
                searchTemplate:
                {
                    template: template
                }
            }
        }).fail((r) => {
            console.error(r);
            deferred.reject(r);
        })
            .done(r => {
                deferred.resolve(r);
            });
        return deferred.promise();
    };



    $("[data-core-global-search-button]").on("click",
        (event) => {
            ExecuteDoSearch("/home/globalsearch", "global");
        });

    $("[data-core-local-search-button]").on("click",
        (event) => {
            ExecuteDoSearch("/home/localsearch", "local");
        });

    $("[data-core-global-searchtemplate-edit]").on("keydown", (event) => {
        if (event.keyCode === 13) {
            ExecuteDoSearch("/home/globalsearch", "global");
        }
    });

    $("[data-core-local-searchtemplate-edit]").on("keydown", (event) => {
        if (event.keyCode === 13) {
            ExecuteDoSearch("/home/localsearch", "local");
        }
    });
});
