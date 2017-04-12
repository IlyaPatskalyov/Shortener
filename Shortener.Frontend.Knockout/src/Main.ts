import "./core/KoComponent";
import "./pages/my-links/MyLinksPage";
import * as ko from "knockout";
import Router = require("director");

class Main {
    constructor() {
        this.page = ko.observable("");
        let _this = this;
        this.router = new Router({
            '/'() {
                require(['pages/my-links/MyLinksPage'], () => {
                    _this.page("my-links");
                    _this.params({});
                });
            },
            '/my-links'() {
                require(['pages/my-links/MyLinksPage'], () => {
                    _this.page("my-links");
                    _this.params({});
                });
            }
        });
        this.router.configure({
            html5history: false
        });
        this.router.init('/');
    }

    page: KnockoutObservable<string>;
    params: KnockoutObservable<any> = ko.observable(null);
    router: Router;
}

document.getElementById("page").innerHTML = ' <!-- ko if: page() --> <div data-bind="component: { name: page, params: params }"></div> <!-- /ko -->';
ko.applyBindings(new Main());