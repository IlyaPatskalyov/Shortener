import "css!./Main";
import "css!bootstrap/bootstrap";
import "css!bootstrap/bootstrap-theme";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { createHashHistory } from "history";
import { Router, Route, Redirect, useRouterHistory } from "react-router";
import {TestPage} from "./pages/test/TestPage";

export module Main {

    const history = useRouterHistory(createHashHistory)({ queryKey: false })

    ReactDOM.render(
        <Router history={history}>
            <Route>
            <Route path="test" component={TestPage}/>
            <Redirect from="/" to="test"/>
            <Route path="*" component={TestPage}/>
        </Route>
    </Router>, document.getElementById("page"));

}