import "text!./AddLinkPage.html";
import "../../components/navigation/NavigationComponent";
import {LinksApi} from "../../api/LinksApi";
import * as ko from "knockout";

@koComponent(ko, {
    name: "add-link",
    template: require("text!./AddLinkPage.html")
})
class AddLinkPage {

    private url: KnockoutObservable<string>;
    private short: KnockoutObservable<string>;


    constructor() {
        this.url = ko.observable<string>();
        this.short = ko.observable<string>();
    }

    private async addLink() {
        try {
            let short = await LinksApi.add(this.url());
            this.short(short);
        }
        catch(e) {
            alert(e.message);
        }
    }
}
export = AddLinkPage;