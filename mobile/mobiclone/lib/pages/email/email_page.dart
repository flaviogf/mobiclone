import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';

class EmailPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider<EmailBloc>(
      create: (_) => kiwi.Container().resolve<EmailBloc>(),
      child: Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.white,
          elevation: 0,
          iconTheme: IconThemeData(
            color: Colors.deepPurple,
          ),
        ),
        backgroundColor: Colors.white,
        body: EmailForm(),
      ),
    );
  }
}

class EmailForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => EmailFormState();
}

class EmailFormState extends State<EmailForm> {
  final TextEditingController _emailController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BlocListener<EmailBloc, EmailState>(
      listener: (context, state) {
        if (state is ValidatedEmailState) {
          Navigator.of(context).pushNamed('/password');
        }

        if (state is RequiredEmailState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }
      },
      child: BlocBuilder<EmailBloc, EmailState>(
        builder: (context, state) {
          return SafeArea(
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
                      controller: _emailController,
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
                    BlocProvider.of<EmailBloc>(context).add(
                      EmailEventSubmit(_emailController.text),
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
          );
        },
      ),
    );
  }
}
