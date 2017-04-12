import "text!./MyLinksPage.html";
import "../../components/navigation/NavigationComponent";
import {LinksApi} from "../../api/LinksApi";
import * as ko from "knockout";

@koComponent(ko, {
    name: "my-links",
    template: require("text!./MyLinksPage.html")
})
class MyLinksPage {

    private loaded: KnockoutObservable<boolean>;
    private myLinks: KnockoutObservableArray<Link>;

    constructor() {
        this.loaded = ko.observable(false);
        this.myLinks = ko.observableArray([]);

        this.load();
    }

    private async load() {
        let links = await LinksApi.getMyLinks();
        this.myLinks(links);
        this.loaded(true);
    }
}
export = MyLinksPage;