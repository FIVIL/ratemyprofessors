import React, { Component } from 'react';
import { Router, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Faculties } from './components/Faculties';
import { ContactUs } from './components/ContactUs';
import { Course } from './components/Course';
import { AddProf } from './components/AddProfessor';
import { AddCourse } from './components/AddCourse';
import { Proff } from './components/Professor'
import { fail } from 'assert';
import history from './components/history';

export default class App extends Component {
    displayName = App.name
    render() {
        return (
            <Layout>
                <Router history={history}>
                    <div>
                        <Route exact path='/' component={Home}/>
                        <Route path='/faculties/:Name/:Name2/:Name3' component={Faculties} />
                        <Route path='/contact' component={ContactUs} />
                        <Route path='/course/:ID/:ID2' component={Course} />
                        <Route path='/AddProf/:ID' component={AddProf} />
                        <Route path='/AddCourse/:ID' component={AddCourse} />
                        <Route path='/Professor/:Name' component={Proff} />
                    </div>
                </Router>
            </Layout>
        );
    }
}
