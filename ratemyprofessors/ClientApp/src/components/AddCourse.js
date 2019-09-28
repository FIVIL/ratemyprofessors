import React, { Component } from 'react';
import { Glyphicon, Col, Grid, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { fail } from 'assert';
import MultiSelect from "@kenshooui/react-multi-select";
import Gist from 'react-gist';
import history from './history';
import { ToastContainer, ToastStore } from 'react-toasts';
import { Footer } from './Footer'
export class AddCourse extends Component {
    displayName = AddCourse.name
    constructor(props) {
        super(props);
        this.state = {
            Name: '', coursesList: [], FacsList: [], loading: true, fac: '', coursesListSelected: [],
            messages: {
                searchPlaceholder: "جستجو",
                noItemsMessage: "برای ظاهر شدن موارد به جستجو بپردازید",
                noneSelectedMessage: "",
                selectedMessage: "پاک کردن",
                selectAllMessage: "انتخاب همه",
                clearAllMessage: "پاک کردن همه",
                disabledItemsTooltip: "شما فقط میتوانید یک فایل انتخاب کنید"
            }
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleMultiSelectourseChange = this.handleMultiSelectourseChange.bind(this);
        this.FetchData();
    }
    handleMultiSelectourseChange(selectedItems) {
        this.setState({ coursesListSelected: selectedItems });
    }
    FetchData() {
        fetch('api/Professors/ProfFac/' + this.props.match.params.ID)
            .then(response => response.json())
            .then(data => {
                data.map(d => {
                    this.state.coursesList.push({ id: d.id, label: d.fullName });
                });
            }).catch(r => console.log(r));
        fetch('api/Courses/facs')
            .then(response => response.json())
            .then(data => {
                data.map(d => {
                    this.state.FacsList.push({ value: d.id, label: d.name });
                });
                this.setState({ loading: false });
            }).catch(r => console.log(r));
    }
    optionClicked(optionsList) {
        this.setState({ CoursesList: optionsList });
    }
    selectedBadgeClicked(optionsList) {
        this.setState({ CoursesList: optionsList });
    }
    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        this.setState({
            [name]: target.value
        });
    }
    handleSubmit(event) {
        event.preventDefault();
        var ffacs = '';
        this.state.coursesListSelected.map(c => {
            if (c.value != false) {
                ffacs += c.id + ';'
            }
        });
        fetch('/api/Courses/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: this.state.Name,
                facID: this.state.fac,
                profs: ffacs,
            })
        }).then(response => response.status == 200 ? ToastStore.success('عملیات با موفقیت انجام شد.', 2000) : ToastStore.error('متاسفانه خطایی رخ داد.', 2000))
            ;
        this.setState({ Name: '', CoursesList: [], fac: '', coursesListSelected: [] });
        fetch('api/Professors/GetByName')
            .then(response => response.json())
            .then(data => {
                data.map(d => {
                    this.state.CoursesList.push({ value: d.id, label: d.name });
                });
            }).catch(r => console.log(r));
    }
    renderDate(facData) {
        return (<div>
            <form onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <label className="control-label">
                        نام
                    </label>
                    <br />
                    <input type="text"
                        placeholder="نام" value={this.state.Name}
                        onChange={this.handleInputChange} name="Name" className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label className="control-label">
                        دانشکده درس
                    </label>
                    <br />
                    <select value={this.state.fac} onChange={this.handleInputChange}
                        className="form-control" name="fac">
                        <option value="">انتخاب کنید</option>
                        {facData.map(ff =>
                            <option value={ff.value}>{ff.label}</option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    <label className="control-label">
                        اساتید درس
</label>
                    <br />
                    <MultiSelect
                        wrapperClassName="form-control MyMultiSelect"
                        items={this.state.coursesList}
                        selectedItems={this.state.coursesListSelected}
                        onChange={this.handleMultiSelectourseChange}
                        messages={this.state.messages}
                        showSelectedItems={false}
                        height={200}
                    />
                </div>
                <br />
                <div className="form-group">
                    <input type="submit" value="پیشنهاد" className="CenterFormElement btn btn-primary" />
                </div>
            </form>
        </div>);
    }
    static RnderFooter() {
        return (<footer className="footer" style={{ background: 'linear-gradient(rgb(68,68,68), rgb(20,20,20))', color: 'white' }}>
            <Grid fluid>
                <Row>
                    <Col md={5}>
                     <Footer />
                    </Col>
                    <Col md={4}>
                        <h3>درباره Grade my teachers</h3>
                        <hr />
                        <p>
                            این سایت در راستای کمک به انتخاب واحد دانشجویان
                                                            دانشگاه صنعتی خواجه نصیر الدین توسی به همت برخی از دانشجویان
                                                                                            دانشکده مهندسی کامپیوتر دانشگاه ایجاد شده است.
                            </p>
                        <p>
                            هدف اصلی این سایت امتیاز دادن به صورت <code>ناشناس</code> و <code>منصفانه</code>   به اساتید دانشکده های مختلف دانشگاه
                            می باشد
                            تا بر اساس این نظرات و امتیاز ها دانشجویان در ترم های  بعد انتخاب های بهتری انجام دهند.
                            </p>
                        <p>



                        </p>
                    </Col>
                    <Col md={3}>
                        <h3>به اشتراک گذاری</h3>
                        <hr />
                        <p>به منظور بهتر شدن سایت به کمک شما دوستان برای
                            به اشتراک گذاری
                             سایت بین دانشجویان دانشگاه نیازمندیم
                            در این راستا میتوانید از طریق پیام رسان تلگرام یا شبکه اجتماعی
                                تویتر مارا یاری کنید.</p>
                        <p className="IconCenter">
                            <a href="https://twitter.com/intent/tweet?text=%D8%A8%D9%87+%D8%A7%D8%B3%D8%AA%D8%A7%D8%AF+%D9%87%D8%A7%D8%AA+%D9%86%D9%85%D8%B1%D9%87+%D8%A8%D8%AF%D9%87+%0A+http://grademyteachers.kntu.qmak.ir//" target="_blank" >
                                <img alt="twitter" src={require('../imgs/twitter.svg')} className="ShareLogoImg" />
                            </a>
                            <a href="https://telegram.me/share/url?url=http://grademyteachers.kntu.qmak.ir//" target="_blank">
                                <img alt="telegram" src={require('../imgs/telegram.svg')} className="ShareLogoImg" />
                            </a>
                        </p>
                        <br />
                        <p>
                            برای ارتباط با ما میتوانید از این <Link to={'/contact'} onClick={() => history.push('/contact')} style={{ backgroundColor: 'rgba(256,256,256,0.2)', padding: '1px 3px', borderRadius: '5px' }}>لینک</Link> استفاده نمایید.
                            </p>
                    </Col>
                </Row>
            </Grid>
            <hr />
            <p className="text-center">
                &copy; 2018 هیچ حقی محفوظ نیست.
                    </p>
        </footer>);
    };
    render() {
        let contents = this.state.loading
            ? <h3><em>در حال بارگزاری...</em></h3>
            : this.renderDate(this.state.FacsList);
        let foot = AddCourse.RnderFooter();
        return (
            <div>
                <ToastContainer store={ToastStore} position={ToastContainer.POSITION.TOP_RIGHT} />
                <div className="container">
                    <br />
                    <h1>
                        پیشنهاد درس جدید
                </h1>
                    <hr />
                    <p className="text-justify PPP text-center">
                        از این فرم می توانید برای پیشنهادافزودن درس جدید استفاده کنید ، درس پیشنهاد شده توسط شما
                        پس از تایید نهایی به سایت افزوده خواهد شد.
                </p>
                    <p className="text-justify PPP text-center">
                        لطفا اطلاعات مورد نیاز را به دقت وارد نمایید ، اگر استاد مورد نظر شما در بین گزینه های زیر
                        موجود نبود می توانید به افزودن استاد نیز بپردازید.
                </p>
                    <p className="text-justify PPP text-primary text-center">
                        پیشاپیش از کمک شما در راستای کامل شدن و هدفمندتر شدن این سایت سپاس گذاریم.
                </p>
                    <br />
                    {contents}
                    <br />
                </div>
                {foot}
            </div>
        );
    }
}
