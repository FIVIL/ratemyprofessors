import React, { Component } from 'react';
import { Glyphicon, Col, Grid, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { fail } from 'assert';
import Gist from 'react-gist';
import history from './history';
import { ToastContainer, ToastStore } from 'react-toasts';
import MultiSelect from "@kenshooui/react-multi-select";
import { Footer } from './Footer'
export class AddProf extends Component {
    displayName = AddProf.name
    constructor(props) {
        super(props);
        this.state = {
            Name: '', CoursesList: [], FacsList: [], LastName: '', Link: '', PrivateLink: '', WP: '',
            Img: '', Comment: '', loading: true, CoursesListSelected: [], FacListSelected: [],
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
        this.handleMultiSelectourseChange = this.handleMultiSelectourseChange.bind(this);
        this.handleMultiSelectFacChange = this.handleMultiSelectFacChange.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.FetchData();
    }
    handleMultiSelectourseChange(selectedItems) {
        this.setState({ coursesListSelected: selectedItems });
    }
    handleMultiSelectFacChange(selectedItems) {
        this.setState({ FacListSelected: selectedItems });
    }
    FetchData() {
        fetch('api/Courses/' + this.props.match.params.ID)
            .then(response => response.json())
            .then(data => {
                data.map(d => {
                    this.state.CoursesList.push({ id: d.id, label: d.name });
                });
            }).catch(r => console.log(r));
        fetch('api/Courses/facs')
            .then(response => response.json())
            .then(data => {
                data.map(d => {
                    this.state.FacsList.push({ id: d.id, label: d.name });
                });
                this.setState({ loading: false });
            }).catch(r => console.log(r));
    }
    optionClickedC(optionsList) {
        this.setState({ CoursesList: optionsList });
    }
    selectedBadgeClickedC(optionsList) {
        this.setState({ CoursesList: optionsList });
    }
    optionClickedF(optionsList) {
        this.setState({ FacsList: optionsList });
    }
    selectedBadgeClickedF(optionsList) {
        this.setState({ FacsList: optionsList });
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
        var cous = '';
        this.state.coursesListSelected.map(c => {
            cous += c.id + ';';
        });
        var facs = '';
        this.state.FacListSelected.map(c => {
            facs += c.id + ';';
        });
        fetch('/api/Professors/NewProf/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: this.state.Name,
                lastName: this.state.LastName,
                link: this.state.Link,
                privateLink: this.state.PrivateLink,
                wpLink: this.state.WP,
                imageLink: this.state.Img,
                comment: this.state.Comment,
                courses: cous,
                facs: facs,
                staff: false,
                approved: false,
            })
        }).then(response => response.status == 200 ? ToastStore.success('عملیات با موفقیت انجام شد.', 2000) : ToastStore.error('متاسفانه خطایی رخ داد.', 2000))
            ;
        this.setState({
            Name: '', CoursesList: [], FacsList: [], LastName: '', Link: '', PrivateLink: '', WP: '',
            Img: '', Comment: '', loading: true, CoursesListSelected: [], FacListSelected: []
        });
        this.FetchData();
    }
    renderDate(facData) {
        const selectedOptionsStyles = {
            color: "#dff0d8",
            backgroundColor: "#17A2B8",
            borderRadius: '3px',
            float: 'right'
        };
        const optionsListStyles = {
            color: "#dff0d8",
            backgroundColor: "#17A2B8",
        };
        return (<div>

            <form onSubmit={this.handleSubmit}>
                <Grid fluid>
                    <Row>
                        <Col md={6}>
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
                                    نام خانوادگی
                    </label>
                                <br />
                                <input type="text"
                                    placeholder="نام خانوادگی" value={this.state.LastName}
                                    onChange={this.handleInputChange} name="LastName" className="form-control"
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    آدرس صفحه شخصی
                    </label>
                                <br />
                                <input type="text"
                                    placeholder="آدرس صفحه شخصی" value={this.state.Link}
                                    onChange={this.handleInputChange} name="Link" className="form-control"
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    دانشکده ها
</label>
                                <br />
                                <MultiSelect
                                    wrapperClassName="form-control MyMultiSelect"
                                    items={this.state.FacsList}
                                    selectedItems={this.state.FacListSelected}
                                    onChange={this.handleMultiSelectFacChange}
                                    messages={this.state.messages}
                                    showSelectedItems={false}
                                    height={200}
                                />
                            </div>
  
                        </Col>
                        <Col md={6}>
                            <div className="form-group">
                                <label className="control-label">
                                    آدرس صفحه WP دانشگاه
                    </label>
                                <br />
                                <input type="text"
                                    placeholder="آدرس صفحه WP دانشگاه" value={this.state.WP}
                                    onChange={this.handleInputChange} name="WP" className="form-control"
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    آدرس تصویر (موجود در سایت دانشکده ها)
                    </label>
                                <br />
                                <input type="text"
                                    placeholder="تصویر" value={this.state.Img}
                                    onChange={this.handleInputChange} name="Img" className="form-control"
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    آدرس ایمیل
                    </label>
                                <br />
                                <input type="text"
                                    placeholder="آدرس ایمیل" value={this.state.PrivateLink}
                                    onChange={this.handleInputChange} name="PrivateLink" className="form-control"
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    درس ها
</label>
                                <br />
                                <MultiSelect
                                    wrapperClassName="form-control MyMultiSelect"
                                    items={this.state.CoursesList}
                                    selectedItems={this.state.CoursesListSelected}
                                    onChange={this.handleMultiSelectourseChange}
                                    messages={this.state.messages}
                                    showSelectedItems={false}
                                    height={200}
                                />
                            </div>
                         
                        </Col>
                        <div className="form-group">
                            <label className="control-label">
                                توضیحات تکمیلی
</label>
                            <br />
                            <textarea
                                value={this.state.Comment}
                                onChange={this.handleInputChange}
                                placeholder="توضیحات "
                                name="Comment" rows="7" className="form-control" />
                        </div>
                        <br />
                        <div className="form-group">
                            <input type="submit" value="پیشنهاد" className="CenterFormElement btn btn-primary" />
                        </div>
                    </Row>
                </Grid>
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
        let contents = this.renderDate(this.state.FacsList);
        let foot = AddProf.RnderFooter();
        return (
            <div>
                <ToastContainer store={ToastStore} position={ToastContainer.POSITION.TOP_RIGHT} />
                <div className="container">
                    <br />
                    <h1>
                        پیشنهاد استاد جدید
                </h1>
                    <hr />
                    <p className="text-justify PPP text-center">
                        از این فرم می توانید برای پیشنهادافزودن استاد جدید استفاده نمایید، استاد افزوده شده
                       توسط شما پس از بررسی و تایید نهایی به لیست اساتید اضافه خواهد شد.
                    </p>
                    <p className="text-justify PPP text-center">
                        برای افزودن استاد جدید لطفا اطلاعات خواسته شده را به درستی وارد نمایید، بیشتر این اطلاعات
                        در صفحه اعضای هیئت علمی سایت دانشکده شما موجود می باشند.
                </p>
                    <p className="text-justify PPP text-center">
                        برای وارد کردن لینک تصویر استاد می توانید از لینک تصویری که در صفحه اعضای هیئت علمی سایت دانشکده است
                        با کلیک راست کردن بر روی تصویر و دریافت لینک استفاده نمایید.
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
