import "css!bootstrap/bootstrap";
import "css!bootstrap/bootstrap-theme";
import "./core/KoComponent";
import "./pages/my-links/MyLinksPage";
import "./pages/add-link/AddLinkPage";
import * as ko from "knockout";
import Router = require("director");

class Main {
    constructor() {
        this.page = ko.observable("");
        this.router = new Router({
            '/': () => {
                    this.page("add-link");
                    this.params({});
                },
            '/my-links': () => {
                    this.page("my-links");
                    this.params({});
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