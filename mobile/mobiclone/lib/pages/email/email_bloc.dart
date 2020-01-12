import 'package:bloc/bloc.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';

class EmailBloc extends Bloc<EmailEvent, EmailState> {
  @override
  EmailState get initialState => InitiatedEmailState();

  @override
  Stream<EmailState> mapEventToState(EmailEvent event) async* {
    if (event is EmailEventSubmit) {
      yield SubmittedEmailState(event.value);

      if (event.value.isEmpty) {
        yield RequiredEmailState(event.value, 'The email must be informed.');
      } else {
        yield ValidatedEmailState(event.value);
      }
    }
  }
}
