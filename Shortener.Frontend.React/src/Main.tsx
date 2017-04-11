import "css!./Main";
import "css!bootstrap/bootstrap";
import "css!bootstrap/bootstrap-theme";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { createHashHistory } from "history";
import { Router, Route, Redirect, useRouterHistory } from "react-router";
import {AddLinkPage} from "./pages/add-link/AddLinkPage";
import {MyLinksPage} from "./pages/my-links/MyLinksPage";

export module Main {

    const history = useRouterHistory(createHashHistory)({ queryKey: false })

    ReactDOM.render(
        <Router history={history}>
            <Route>
                <Route path="/" component={AddLinkPage}/>
                <Route path="/my-links" component={MyLinksPage}/>
                <Redirect from="*" to="/"/>
            </Route>
        </Router>, document.getElementById("page"));

}