import * as React from "react";
import {Button, Jumbotron} from "react-bootstrap";
import {Navigation} from "../../components/navigation/Navigation";
import {Panel} from "react-bootstrap";

export class TestPage extends React.Component<{}, {}> {

    render() {

        return (<div>
            <Navigation/>
            <div className="container body-content">
                <Jumbotron>
                    <h1>Hello, world!</h1>
                    <p>
                        This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
                    <p><Button bsStyle="primary">Learn more</Button></p>
                </Jumbotron>
                <Panel header={<h3>Panel title</h3>} bsStyle="primary">
                    Panel content
                </Panel>
            </div>

        </div>);

    }
}