import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';

class NamePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider<NameBloc>(
      create: (_) => kiwi.Container().resolve<NameBloc>(),
      child: Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.white,
          elevation: 0,
          iconTheme: IconThemeData(
            color: Colors.deepPurple,
          ),
        ),
        backgroundColor: Colors.white,
        body: NameForm(),
      ),
    );
  }
}

class NameForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => NameFormState();
}

class NameFormState extends State<NameForm> {
  final TextEditingController _nameController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BlocListener<NameBloc, NameState>(
      listener: (context, state) {
        if (state is ValidatedNameState) {
          Navigator.of(context).pushNamed('/email');
        }

        if (state is RequiredNameState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }
      },
      child: BlocBuilder<NameBloc, NameState>(
        builder: (context, state) {
          return SafeArea(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Padding(
                  padding: EdgeInsets.all(16.0),
                  child: Text(
                    'Enter your name',
                    style: TextStyle(
                      color: Colors.black54,
                      fontSize: 36.0,
                    ),
                  ),
                ),
                Expanded(
                  flex: 1,
                  child: Padding(
                    padding: EdgeInsets.all(16.0),
                    child: TextFormField(
                      textAlignVertical: TextAlignVertical.center,
                      controller: _nameController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'João',
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
                    BlocProvider.of<NameBloc>(context).add(
                      SubmitNameEvent(_nameController.text),
                    );
                  },
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      Text(
                        'Next',
                        style: TextStyle(
                          fontSize: 20.0,
                        ),
                      ),
                      Icon(
                        Icons.arrow_forward,
                      ),
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
