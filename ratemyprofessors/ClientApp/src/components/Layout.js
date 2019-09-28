import React, { Component } from 'react';
import { Col, Grid, Row, Glyphicon } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { NavMenu } from './NavMenu';
import Gist from 'react-gist'
import history from './history'
export class Layout extends Component {
    displayName = Layout.name
    render() {
        return (
            <div>
                <NavMenu />
                {this.props.children}
            </div>
        );
    }
}
