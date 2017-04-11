import * as React from "react";
import {Navigation} from "../../components/navigation/Navigation";
import {Button, Jumbotron, Input, Col, Row, Grid, Glyphicon} from "react-bootstrap";
import {LinksApi} from "../../api/LinksApi";

export class AddLinkPage extends React.Component<{}, {url?: string, short?: string}> {

    constructor(props: {}, context: {}) {
        super(props, context)
        this.state = {};
    }

    render() {
        return (<div>
            <Navigation/>
            <div className="container body-content">
                <Jumbotron>
                    <h2>Simplify your links</h2>
                    {!this.state.short ?
                        <div>
                            <Input type="text" bsSize="lg" value={this.state.url}
                                   placeholder="Your original URL here"
                                   onChange={e => this.setState({ url: e.target.value })}
                            />
                            <p>
                                <small>All URLs and click analytics are public and can be accessed by anyone</small>
                            </p>
                            <Button bsStyle="primary" bsSize="large"
                                    disabled={!this.state.url || this.state.url.length == 0}
                                    onClick={async e => {
                                        let short = await LinksApi.add(this.state.url);
                                        this.setState({ short: short });

                                        e.preventDefault();
                                     }}>Shorten URL</Button>
                        </div>
                     :  <div>
                            <h2>{window.location.origin}/{this.state.short}</h2>
                        </div>}
                </Jumbotron>
                <Grid>
                    <Row>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="link"/> Shorten</h2>
                            <p>Shorten your URL so it’s ready to be shared everywhere</p>
                        </Col>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="send"/> Track</h2>
                            <p>Analytics help you know where your clicks are coming from</p>
                        </Col>
                        <Col lg={4}>
                            <h2><Glyphicon glyph="user"/> Learn</h2>
                            <p>Understand and visualize your audience</p>
                        </Col>
                    </Row>
                </Grid>
            </div>
        </div>);

    }
}