import * as React from "react";
import {Navbar, Nav, NavItem} from "react-bootstrap";
import "css!./Navigation"

export class Navigation extends React.Component<{}, {}> {

    render() {
        return (<Navbar inverse={true} fixedTop={true}>
            <Navbar.Header>
                <Navbar.Brand>
                    <a href="#">URL Shortener</a>
                </Navbar.Brand>
            </Navbar.Header>
            <Navbar.Collapse>
                <Nav>
                    <NavItem eventKey={1} href="#/my-links">My links</NavItem>
                </Nav>
            </Navbar.Collapse>
        </Navbar>);
    }
}