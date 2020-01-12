import 'package:flutter/material.dart';
import 'package:mobiclone/pages/password/password_page.dart';

class EmailPage extends StatefulWidget {
  final String name;

  const EmailPage({
    Key key,
    this.name,
  }) : super(key: key);

  @override
  State<StatefulWidget> createState() => EmailPageState();
}

class EmailPageState extends State<EmailPage> {
  @override
  Widget build(BuildContext context) {
    debugPrint(widget.name);

    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.white,
        elevation: 0,
        iconTheme: IconThemeData(
          color: Colors.deepPurple,
        ),
      ),
      backgroundColor: Colors.white,
      body: SafeArea(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Padding(
              padding: EdgeInsets.all(16.0),
              child: Text(
                'Enter your email',
                style: TextStyle(
                  color: Colors.black54,
                  fontSize: 36,
                ),
              ),
            ),
            Expanded(
              flex: 1,
              child: Padding(
                padding: EdgeInsets.all(16.0),
                child: TextField(
                  textAlignVertical: TextAlignVertical.center,
                  decoration: InputDecoration(
                    border: InputBorder.none,
                    hintText: 'joao@email.com',
                    hintStyle: TextStyle(
                      color: Colors.black26,
                    ),
                  ),
                  style: TextStyle(
                    color: Colors.black54,
                    fontSize: 36.0,
                  ),
                  maxLines: null,
                  expands: true,
                ),
              ),
            ),
            RaisedButton(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(0),
              ),
              padding: EdgeInsets.all(24.0),
              onPressed: () {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (_) => PasswordPage(),
                  ),
                );
              },
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  Text(
                    'Next',
                    style: TextStyle(
                      fontSize: 20,
                    ),
                  ),
                  Icon(Icons.arrow_forward),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
