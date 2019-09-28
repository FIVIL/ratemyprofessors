import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem, Form, FormControl, FormGroup, NavDropdown, MenuItem, Button, ButtonToolbar } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import AsyncSelect from 'react-select/lib/Async';
import { fail } from 'assert';
import history from './history'

type State = {
    inputValue: string,
};
var waiting = true;
var showRes = [];
const ProfOptions = () => {
    if (!waiting) return showRes;
    waiting = false;
    fetch('api/Professors/GetByName')
        .then(response => response.json())
        .then(data => {
            data.map(prof => {
                showRes.push({ value: '/Professor/' + prof.id+'/', label: String(prof.fullName), })
            })
        });
    fetch('api/Courses')
        .then(response => response.json())
        .then(data => {
            data.map(course => {
                showRes.push({ value: '/course/' + course.id + '/' + course.name, label: String('درس ' + course.name), })
            })
        });
    return showRes;
}
const StartSearch = (inputValue: string) => ProfOptions().filter(i =>
    i.label.includes(inputValue)
);
const loadOptions = (inputValue, callback) => {
    setTimeout(() => {
        callback(StartSearch(inputValue));
    }, 1000);
};

export class NavMenu extends Component {
    displayName = NavMenu.name
    state = { inputValue: '' };
    handleInputChange = (newValue: string) => {
        const inputValue = newValue;
        this.setState({ inputValue });
        return inputValue;
    };
    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'} onClick={() => history.push('/')}>
                            <text className="HeaderText">به استاد هات چند میدی؟!!</text>
                        </Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/'} exact onClick={() => history.push('/')}>
                            <NavItem>
                                <Glyphicon glyph='home' /> خانه
              </NavItem>
                        </LinkContainer>
                        <NavDropdown title="دانشکده ها">
                            <LinkContainer to={'/faculties/ce/دانشکده مهندسی کامپیوتر/heart'} onClick={() => history.push('/faculties/ce/دانشکده مهندسی کامپیوتر/heart')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='heart' />دانشکده مهندسی کامپیوتر</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/ee/دانشکده مهندسی برق/flash'} onClick={() => history.push('/faculties/ee/دانشکده مهندسی برق/flash')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='flash' />دانشکده مهندسی برق</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/ge/دانشکده مهندسی ژئودزی و ژئوماتیک/globe'} onClick={() => history.push('/faculties/ge/دانشکده مهندسی ژئودزی و ژئوماتیک/globe')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='globe' />دانشکده مهندسی ژئودزی و ژئوماتیک</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/in/دانشکده مهندسی صنایع/wrench'} onClick={() => history.push('/faculties/in/دانشکده مهندسی صنایع/wrench')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='wrench' />دانشکده مهندسی صنایع</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/civ/دانشکده مهندسی عمران/home'} onClick={() => history.push('/faculties/civ/دانشکده مهندسی عمران/home')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='home' />دانشکده مهندسی عمران</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/mec/دانشکده مهندسی مکانیک/cog'} onClick={() => history.push('/faculties/mec/دانشکده مهندسی مکانیک/cog')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='cog' />دانشکده مهندسی مکانیک</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/mse/دانشکده مهندسی مواد/tint'} onClick={() => history.push('/faculties/mse/دانشکده مهندسی مواد/tint')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='tint' />دانشکده مهندسی مواد</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/ae/دانشکده مهندسی هوافضا/plane'} onClick={() => history.push('/faculties/ae/دانشکده مهندسی هوافضا/plane')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='plane' />دانشکده مهندسی هوافضا</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/m/دانشکده ریاضی/plus'} onClick={() => history.push('/faculties/m/دانشکده ریاضی/plus')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='plus' />دانشکده ریاضی</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/ch/دانشکده شیمی/fire'} onClick={() => history.push('/faculties/ch/دانشکده شیمی/fire')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='fire' />دانشکده شیمی</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/ph/دانشکده فیزیک/scale'} onClick={() => history.push('/faculties/ph/دانشکده فیزیک/scale')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='scale' />دانشکده فیزیک</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/gtc/مرکز آموزش های عمومی/blackboard'} onClick={() => history.push('/faculties/gtc/مرکز آموزش های عمومی/blackboard')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='blackboard' />مرکز آموزش های عمومی</MenuItem>
                            </LinkContainer>
                            <LinkContainer to={'/faculties/st/کارکنان آموزش/thumbs-down'} onClick={() => history.push('/faculties/st/کارکنان آموزش/thumbs-down')}>
                                <MenuItem className="DropDownItem" ><Glyphicon glyph='thumbs-down' />کارکنان آموزش</MenuItem>
                            </LinkContainer>
                        </NavDropdown>
                    </Nav>
                    <Navbar.Form pullRight>
                        <AsyncSelect
                            loadOptions={loadOptions}
                            onInputChange={this.handleInputChange}
                            placeholder='جستجو بین اساتید یا دروس'
                            isRtl={true}
                            className="SearchBox"
                            onChange={({ value }) => history.push(value)}
                            noOptionsMessage={() => 'موردی یافت نشد.'}
                            loadingMessage={()=>'در حال جستجو...'}
                        />
                    </Navbar.Form>
                    <LinkContainer to={'/AddProf'} onClick={() => history.push('/AddProf/1')} className="btn btn-primary NewButtons"><div><Glyphicon glyph='plus' style={{ marginLeft:'5px' }} />استاد جدید</div></LinkContainer>
                    {/*<LinkContainer to={'/AddCourse'} onClick={() => history.push('/AddCourse')} className="btn btn-primary NewButtons" ><div><Glyphicon glyph='plus' style={{ marginLeft: '5px' }}/>  درس جدید</div></LinkContainer>*/}
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
