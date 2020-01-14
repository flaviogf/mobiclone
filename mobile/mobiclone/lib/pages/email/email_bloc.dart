import 'package:bloc/bloc.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';

class EmailBloc extends Bloc<EmailEvent, EmailState> {
  final UserRepository _userRepository;

  EmailBloc(this._userRepository);

  @override
  EmailState get initialState => InitiatedEmailState();

  @override
  Stream<EmailState> mapEventToState(EmailEvent event) async* {
    if (event is SubmitEmailEvent) {
      yield SubmittedEmailState(event.value);

      if (event.value.isEmpty) {
        yield RequiredEmailState(event.value, 'The email must be informed.');
      } else {
        final String email = await _userRepository.addEmail(event.value);
        yield ValidatedEmailState(email);
      }
    }
  }
}
